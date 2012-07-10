using System;
using System.Collections.Generic;
using System.Linq;
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

            StorageTreeName = "default";

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public string StorageTreeName { get; set; }

        public void Start()
        {
            Logger.Info("Starting Data Point Writer");

            var eventsByBucket = dataPointStream.DataPoints
                .GroupBy(e => e.Bucket);

            eventBucketSubscription = eventsByBucket.Subscribe(s =>
            {
                var bucketReference = s.Key;
                var dataPoints = s.Select(e => e.DataPoint);

                var storageStrategy = storageStrategyFactory.Build(bucketReference);
                var observable = storageStrategy.Apply(dataPoints);

                observable.Subscribe(e => PersistEvents(bucketReference, e));
            });
        }

        private void PersistEvents(
            BucketReference bucketReference,
            IEnumerable<DataPoint> dataPoints)
        {
            Logger.DebugFormat("Writing down {0} data points", dataPoints.Count());

            var tree = storageEngine.GetOrCreateTree(StorageTreeName);
            var node = tree.GetOrCreateNode(bucketReference.Name);

            var collection = new DataPointCollection(dataPoints.ToList());

            node.Store(collection);
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