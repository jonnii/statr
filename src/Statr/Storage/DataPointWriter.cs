using System;
using System.Linq;
using System.Reactive.Linq;
using Castle.Core.Logging;
using Statr.Routing;
using Statr.Storage.Strategies;

namespace Statr.Storage
{
    public class DataPointWriter : IDataPointWriter
    {
        private readonly IDataPointStream dataPointStream;

        private readonly IStorageEngineFactory storageEngineFactory;

        public DataPointWriter(
            IDataPointStream dataPointStream,
            IStorageEngineFactory storageEngineFactory)
        {
            this.dataPointStream = dataPointStream;
            this.storageEngineFactory = storageEngineFactory;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            var storageEngine = storageEngineFactory.Create(@"c:\dev\tmp\integration-tests");

            Logger.Debug("Starting data point writer");

            var eventsByBucket = dataPointStream.DataPoints
                .GroupBy(e => e.Bucket);

            eventsByBucket.Subscribe(s =>
            {
                var strategy = GetStorageStrategy(s.Key);
                var observable = strategy.Apply(s);

                observable.Subscribe(e =>
                {
                    Logger.DebugFormat("Writing down {0} events", e.Count());

                    var tree = storageEngine.CreateTree("default");
                    var node = tree.CreateNode(s.Key.Name);
                    var dataPoints = e.Select(k => k.DataPoint);

                    node.Store(dataPoints);
                });
            });
        }

        private IStorageStrategy GetStorageStrategy(BucketReference key)
        {
            return new ImmediateStorageStrategy();
        }
    }
}