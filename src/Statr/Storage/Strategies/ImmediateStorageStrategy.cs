using Castle.Core.Logging;

namespace Statr.Storage.Strategies
{
    public class ImmediateStorageStrategy : IStorageStrategy
    {
        private readonly IStorageEngine storageEngine;

        public ImmediateStorageStrategy(IStorageEngine storageEngine)
        {
            this.storageEngine = storageEngine;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Push(BucketReference bucket, DataPoint dataPoint)
        {
            var tree = storageEngine.CreateTree("default", c => { });
            var node = tree.CreateNode(bucket.Name, c => { });

            Logger.Debug("immediately storing datapoints");

            node.Store(new[] { dataPoint });
        }
    }
}
