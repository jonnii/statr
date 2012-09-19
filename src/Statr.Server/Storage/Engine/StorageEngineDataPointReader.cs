using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public class StorageEngineDataPointReader : IDataPointReader
    {
        private readonly IStorageTree storageTree;

        private readonly BucketReference bucketReference;

        public StorageEngineDataPointReader(IStorageTree storageTree, BucketReference bucketReference)
        {
            this.storageTree = storageTree;
            this.bucketReference = bucketReference;
        }

        public IEnumerable<DataPoint> Read()
        {
            var node = storageTree.GetOrCreateNode(bucketReference.Name);
            return node.Read();
        }
    }
}