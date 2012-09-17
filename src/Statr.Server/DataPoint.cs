using System;

namespace Statr.Server
{
    public class DataPoint
    {
        public DataPoint(DateTime timeStamp, float? value, uint numMetrics)
            : this(timeStamp.Ticks, value, numMetrics)
        {
        }

        public DataPoint(long timeStamp, float? value, uint numMetrics)
        {
            TimeStamp = timeStamp;
            Value = value;
            NumMetrics = numMetrics;
        }

        public long TimeStamp { get; set; }

        public uint NumMetrics { get; set; }

        public float? Value { get; set; }

        public override string ToString()
        {
            return string.Concat("[DataPoint TimeStamp=", TimeStamp, " Value=", Value, " NumMetrics=", NumMetrics, "]");
        }
    }
}