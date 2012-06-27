using System;

namespace Statr
{
    public class Metric
    {
        public static Metric Parse(string raw)
        {
            var colon = raw.IndexOf(":", StringComparison.Ordinal);
            var name = raw.Substring(0, colon);

            if (raw.EndsWith("c"))
            {
                var pipe = raw.IndexOf("|", StringComparison.Ordinal);
                var value = raw.Substring(colon + 1, pipe - colon - 1);

                return new Metric(name, float.Parse(value), MetricType.Count);
            }

            if (raw.EndsWith("g"))
            {
                var pipe = raw.IndexOf("|", StringComparison.Ordinal);
                var value = raw.Substring(colon + 1, pipe - colon - 1);

                return new Metric(name, float.Parse(value), MetricType.Gauge);
            }

            throw new NotSupportedException("metric not yet supported");
        }

        public Metric(string name, float value, MetricType metricType)
        {
            Name = name;
            TimeStamp = DateTime.UtcNow;
            Value = value;
            MetricType = metricType;
        }

        public DateTime TimeStamp { get; private set; }

        public string Name { get; private set; }

        public float Value { get; private set; }

        public MetricType MetricType { get; private set; }
    }
}