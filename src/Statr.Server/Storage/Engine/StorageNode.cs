using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Statr.Server.Storage.Engine
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

        public void Write(IEnumerable<DataPoint> dataPoints)
        {
            var firstPoint = dataPoints.First();
            var firstStamp = firstPoint.TimeStamp;

            var slice = CreateSlice(firstStamp, 1);

            var startTime = dataPoints.First().TimeStamp;
            var dataPointValues = dataPoints.Select(d => d.Value.Value).ToArray();
            var sliceData = new SliceData(startTime, dataPointValues);

            slice.Write(sliceData);
        }

        public IEnumerable<DataPoint> Read()
        {
            return GetSlices().Select(s => s.Read()).SelectMany(d => d.ToDataPoints());
        }

        public IEnumerable<IStorageSlice> GetSlices()
        {
            var info = new DirectoryInfo(FilePath);
            var slicefiles = info.GetFiles("*.slice");

            return slicefiles.Select(sliceFile => new StorageSlice(this, sliceFile.Name));
        }

        public IStorageSlice CreateSlice(long startTime, int timeStep)
        {
            var slice = new StorageSlice(this, startTime, timeStep);
            slice.Touch();
            return slice;
        }
    }
}