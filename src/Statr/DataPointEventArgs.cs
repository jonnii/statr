using System;
using Statr.Routing;

namespace Statr
{
    public class DataPointEventArgs : EventArgs
    {
        public DataPointEventArgs(Bucket bucket, DataPoint dataPoint)
        {
            Bucket = bucket;
            DataPoint = dataPoint;
        }

        public Bucket Bucket { get; private set; }

        public DataPoint DataPoint { get; private set; }
    }
}