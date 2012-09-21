using System;
using System.Reactive.Linq;
using Castle.Core.Logging;
using Statr.Server.Routing;

namespace Statr.Server.Storage
{
    public class DataPointBuffer : IDisposable
    {
        private readonly IDataPointStream dataPointStream;

        private readonly IBufferStrategyFactory bufferStrategyFactory;

        private readonly IStorageEngine storageEngine;

        private IDisposable bucketSubscription;

        public DataPointBuffer(
            IDataPointStream dataPointStream,
            IBufferStrategyFactory bufferStrategyFactory,
            IStorageEngine storageEngine)
        {
            this.dataPointStream = dataPointStream;
            this.bufferStrategyFactory = bufferStrategyFactory;
            this.storageEngine = storageEngine;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            Logger.Info("Starting Data Point Buffer");

            var bucketedEvents = dataPointStream.DataPoints
                .GroupBy(e => e.Bucket);

            bucketSubscription = bucketedEvents.Subscribe(s =>
            {
                var bucketReference = s.Key;

                var storageStrategy = bufferStrategyFactory.Build(bucketReference);

                var dataPoints = s.Select(e => e.DataPoint);
                var storableDataPoints = storageStrategy.Apply(dataPoints);

                var bucketWriter = storageEngine.GetWriter(bucketReference);
                storableDataPoints.Subscribe(bucketWriter.Write);
            });
        }

        public void Dispose()
        {
            Logger.Info("Disposing Data Point Buffer");

            if (bucketSubscription != null)
            {
                bucketSubscription.Dispose();
            }
        }
    }
}