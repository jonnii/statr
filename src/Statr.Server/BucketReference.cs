namespace Statr.Server
{
    public class BucketReference
    {
        public BucketReference(MetricType metricType, string name)
        {
            Name = name;
            MetricType = metricType;
        }

        public string Name { get; private set; }

        public MetricType MetricType { get; private set; }

        public string Key
        {
            get
            {
                return string.Concat(MetricType, "/", Name);
            }
        }

        public override string ToString()
        {
            return string.Concat("[BucketReference Name=", Name, " MetricType=", MetricType, "]");
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

            return obj.GetType() == GetType() && Equals((BucketReference)obj);
        }

        protected bool Equals(BucketReference other)
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