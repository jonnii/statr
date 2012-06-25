using System;

namespace Statr.Routing
{
    public class MetricAggregation
    {
        public long LastValue { get; set; }

        public DataPoint ToDataPoint()
        {
            return new DataPoint(DateTime.Now, LastValue);
        }
    }
}