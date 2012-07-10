using System;
using System.Reactive.Linq;
using Castle.Core.Logging;
using Statr.Routing;

namespace Statr.Storage
{
    public class DataPointWriter : IDisposable
    {
        private readonly IDataPointStream dataPointStream;

        private readonly IStorageStrategyFactory storageStrategyFactory;

        private readonly IStorageEngine storageEngine;

        private IDisposable eventBucketSubscription;

        public DataPointWriter(
            IDataPointStream dataPointStream,
            IStorageStrategyFactory storageStrategyFactory,
            IStorageEngine storageEngine)
        {
            this.dataPointStream = dataPointStream;
            this.storageStrategyFactory = storageStrategyFactory;
            this.storageEngine = storageEngine;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            Logger.Info("Starting Data Point Writer");

            var eventsByBucket = dataPointStream.DataPoints
                .GroupBy(e => e.Bucket);

            eventBucketSubscription = eventsByBucket.Subscribe(s =>
            {
                var bucketReference = s.Key;

                var storageStrategy = storageStrategyFactory.Build(bucketReference);

                var dataPoints = s.Select(e => e.DataPoint);
                var storableDataPoints = storageStrategy.Apply(dataPoints);

                var bucketWriter = storageEngine.GetWriter(bucketReference);

                storableDataPoints.Subscribe(bucketWriter.Write);
            });
        }

        public void Dispose()
        {
            Logger.Info("Disposing data point writer");

            if (eventBucketSubscription != null)
            {
                eventBucketSubscription.Dispose();
            }
        }
    }
}