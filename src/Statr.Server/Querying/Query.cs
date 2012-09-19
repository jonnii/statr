namespace Statr.Server.Querying
{
    public class Query
    {
        public Query(string bucketName, MetricType metricType)
        {
            BucketName = bucketName;
            MetricType = metricType;
        }

        public string BucketName { get; private set; }

        public MetricType MetricType { get; private set; }

        public BucketReference BucketReference
        {
            get { return new BucketReference(BucketName, MetricType); }
        }

        public override string ToString()
        {
            return string.Concat("[Query BucketName=", BucketName, " MetricType=", MetricType, "]");
        }
    }
}