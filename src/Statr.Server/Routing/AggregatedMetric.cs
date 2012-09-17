using System;

namespace Statr.Routing
{
    public class AggregatedMetric
    {
        public uint NumMetrics { get; set; }

        public float LastValue { get; set; }

        public float Value { get; set; }

        public bool HasMetrics
        {
            get { return NumMetrics != 0; }
        }

        public DataPoint ToDataPoint()
        {
            return new DataPoint(DateTime.UtcNow, Value, NumMetrics);
        }
    }
}