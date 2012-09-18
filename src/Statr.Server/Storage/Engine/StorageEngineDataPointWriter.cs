using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public class StorageEngineDataPointWriter : IDataPointWriter
    {
        private readonly IStorageTreeRoot storageTreeRoot;

        private readonly BucketReference bucketReference;

        public StorageEngineDataPointWriter(
            IStorageTreeRoot storageTreeRoot,
            BucketReference bucketReference)
        {
            this.storageTreeRoot = storageTreeRoot;
            this.bucketReference = bucketReference;
        }

        public void Write(IEnumerable<DataPoint> dataPoints)
        {
            var tree = storageTreeRoot.GetOrCreateTree("default/" + bucketReference.MetricType);
            var node = tree.GetOrCreateNode(bucketReference.Name);

            node.Store(dataPoints);
        }
    }
}