using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Statr.Storage
{
    public class StorageNode : IStorageNode
    {
        private readonly IStorageNodeConfiguration nodeConfiguration;

        public StorageNode(
            IStorageTree storageTree,
            string name,
            IStorageNodeConfiguration nodeConfiguration)
        {
            this.nodeConfiguration = nodeConfiguration;
            FilePath = Path.Combine(storageTree.FilePath, name);
        }

        public string FilePath { get; private set; }

        public void Initialize()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            var metaData = JsonConvert.SerializeObject(nodeConfiguration);
            File.WriteAllText(Path.Combine(FilePath, ".metadata"), metaData);
        }

        public void Store(DataPointCollection points)
        {
            var firstPoint = points.First();
            var firstStamp = firstPoint.TimeStamp;

            var slice = CreateSlice(firstStamp, 1);
            slice.Write(points.ToSliceData());
        }

        public IStorageSlice CreateSlice(long startTime, int timeStep)
        {
            var slice = new StorageSlice(this, startTime, timeStep);
            slice.Touch();
            return slice;
        }
    }
}