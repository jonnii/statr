using System;

namespace Statr.Server
{
    public class Metric
    {
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

        public BucketReference ToBucket()
        {
            return new BucketReference(Name, MetricType);
        }
    }
}