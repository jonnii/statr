using System;

namespace Statr
{
    public class DataPointEventArgs : EventArgs
    {
        public DataPointEventArgs(DataPoint dataPoint)
        {
            DataPoint = dataPoint;
        }

        public DataPoint DataPoint { get; private set; }
    }
}