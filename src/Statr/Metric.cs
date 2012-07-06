using System;
using Statr.Routing;

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

        public static Metric Count(string name, float value)
        {
            return new Metric(name, value, MetricType.Count);
        }

        public static Metric Gauge(string name, float value)
        {
            return new Metric(name, value, MetricType.Gauge);
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

        public override string ToString()
        {
            return string.Concat("[Metric Name=", Name, " Value=", Value, " MetricType=", MetricType, "]");
        }

        public Bucket ToBucket()
        {
            return new Bucket(Name, MetricType);
        }
    }
}