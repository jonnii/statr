using System;

namespace Statr.Server
{
    public static class MetricTypeParser
    {
        public static MetricType Parse(string metricType)
        {
            switch (metricType)
            {
                case "gauge":
                case "Gauge":
                    return MetricType.Gauge;
                case "count":
                case "Count":
                    return MetricType.Count;
                default:
                    throw new NotSupportedException(
                        string.Format("Could not parse the metric type: {0}", metricType));
            }
        }
    }
}