using System;
using System.IO;

namespace Statr.Server.Storage.Engine
{
    public class StorageSlice : IStorageSlice
    {
        private readonly IStorageNode storageNode;

        public StorageSlice(IStorageNode storageNode, long startTime, int timeStep)
        {
            this.storageNode = storageNode;
            StartTime = startTime;
            TimeStep = timeStep;
        }

        public long StartTime { get; set; }

        public long TimeStep { get; set; }

        public string SliceName
        {
            get { return string.Format("{0}@{1}.slice", StartTime, TimeStep); }
        }

        public string SlicePath
        {
            get { return Path.Combine(storageNode.FilePath, SliceName); }
        }

        public void Touch()
        {
            File.Create(SlicePath).Dispose();
        }

        public void Write(SliceData sliceData)
        {
            var dataPoints = sliceData.DataPoints;

            if (dataPoints.Length == 0)
            {
                throw new ArgumentException("There are no data points to write down", "sliceData");
            }

            var result = new byte[dataPoints.Length * sizeof(float)];
            Buffer.BlockCopy(dataPoints, 0, result, 0, result.Length);

            using (var file = File.OpenWrite(SlicePath))
            {
                file.Write(result, 0, result.Length);
            }
        }

        public long[] Read()
        {
            using (var file = File.OpenRead(SlicePath))
            {
                var length = (int)file.Length;

                var buffer = new byte[length];
                file.Read(buffer, 0, length);

                var values = new long[length / sizeof(long)];
                Buffer.BlockCopy(buffer, 0, values, 0, length);

                return values;
            }
        }
    }
}