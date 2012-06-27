namespace Statr.Routing
{
    public class RouteKey
    {
        public RouteKey(string name, MetricType metricType)
        {
            Name = name;
            MetricType = metricType;
        }

        public string Name { get; private set; }

        public MetricType MetricType { get; private set; }

        public override string ToString()
        {
            return string.Concat("[RouteKey Name=", Name, " MetricType=", MetricType, "]");
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

            return obj.GetType() == GetType() && Equals((RouteKey)obj);
        }

        protected bool Equals(RouteKey other)
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