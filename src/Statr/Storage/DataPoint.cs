using System;

namespace Statr.Storage
{
    public class DataPoint
    {
        public DataPoint(DateTime timeStamp, long? value)
        {
            TimeStamp = timeStamp.Ticks;
            Value = value;
        }

        public DataPoint(long timeStamp, long? value)
        {
            TimeStamp = timeStamp;
            Value = value;
        }

        public long TimeStamp { get; set; }

        public long? Value { get; set; }
    }
}