using System;

namespace Statr
{
    public class DataPoint
    {
        public DataPoint(DateTime timeStamp, float? value)
        {
            TimeStamp = timeStamp.Ticks;
            Value = value;
        }

        public DataPoint(long timeStamp, float? value)
        {
            TimeStamp = timeStamp;
            Value = value;
        }

        public long TimeStamp { get; set; }

        public float? Value { get; set; }
    }
}