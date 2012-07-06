using System;

namespace Statr
{
    public class DataPointEventArgs : EventArgs
    {
        public DataPointEventArgs(BucketReference bucket, DataPoint dataPoint)
        {
            Bucket = bucket;
            DataPoint = dataPoint;
        }

        public BucketReference Bucket { get; private set; }

        public DataPoint DataPoint { get; private set; }
    }
}