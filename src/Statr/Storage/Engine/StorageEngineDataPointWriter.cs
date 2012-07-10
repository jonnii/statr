using System.Collections.Generic;

namespace Statr.Storage.Engine
{
    public class StorageEngineDataPointWriter : IDataPointWriter
    {
        private readonly StorageEngine storageEngine;

        private readonly BucketReference bucketReference;

        public StorageEngineDataPointWriter(
            StorageEngine storageEngine,
            BucketReference bucketReference)
        {
            this.storageEngine = storageEngine;
            this.bucketReference = bucketReference;
        }

        public void Write(IEnumerable<DataPoint> dataPoints)
        {
            var tree = storageEngine.GetOrCreateTree("default");
            var node = tree.GetOrCreateNode(bucketReference.Name);

            node.Store(dataPoints);
        }
    }
}