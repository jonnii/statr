using System;

namespace Statr.Routing
{
    public class AggregatedMetric
    {
        public int NumMetrics { get; set; }

        public long LastValue { get; set; }

        public long Value { get; set; }

        public bool HasMetrics
        {
            get { return NumMetrics != 0; }
        }

        public DataPoint ToDataPoint()
        {
            return new DataPoint(DateTime.UtcNow, Value);
        }
    }
}