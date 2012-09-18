using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public class StorageEngineDataPointWriter : IDataPointWriter
    {
        private readonly IStorageTree tree;

        private readonly BucketReference bucketReference;

        public StorageEngineDataPointWriter(
            IStorageTree tree,
            BucketReference bucketReference)
        {
            this.tree = tree;
            this.bucketReference = bucketReference;
        }

        public void Write(IEnumerable<DataPoint> dataPoints)
        {
            var node = tree.GetOrCreateNode(bucketReference.Name);

            node.Store(dataPoints);
        }
    }
}