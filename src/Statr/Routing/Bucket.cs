using System;

namespace Statr.Routing
{
    public class Bucket
    {
        public Bucket(string name, MetricType metricType)
        {
            Name = name;
            MetricType = metricType;
        }

        public string Name { get; private set; }

        public MetricType MetricType { get; private set; }

        public IAggregationStrategy BuildAggregationStrategy()
        {
            switch (MetricType)
            {
                case MetricType.Count:
                    return new AccumulateAggregationStrategy();
                case MetricType.Gauge:
                    return new LastValueAggregationStrategy();
                default:
                    throw new NotSupportedException(
                        string.Format("The metric type is unsupported: {0}", MetricType));
            }
        }

        public override string ToString()
        {
            return string.Concat("[Bucket Name=", Name, " MetricType=", MetricType, "]");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Bucket)obj);
        }

        protected bool Equals(Bucket other)
        {
            return string.Equals(Name, other.Name) && MetricType.Equals(other.MetricType);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name != null ? Name.GetHashCode() : 0) * 397 ^ MetricType.GetHashCode();
            }
        }
    }
}