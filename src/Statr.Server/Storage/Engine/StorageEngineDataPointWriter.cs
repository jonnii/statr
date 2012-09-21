using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public class StorageEngineDataPointWriter : IDataPointWriter
    {
        private readonly IStorageTree storageTree;

        private readonly BucketReference bucketReference;

        public StorageEngineDataPointWriter(
            IStorageTree storageTree,
            BucketReference bucketReference)
        {
            this.storageTree = storageTree;
            this.bucketReference = bucketReference;
        }

        public void Write(IEnumerable<DataPoint> dataPoints)
        {
            var node = storageTree.GetOrCreateNode(bucketReference.Name);
            node.Write(dataPoints);
        }
    }
}