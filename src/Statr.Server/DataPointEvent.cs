namespace Statr.Server
{
    public class DataPointEvent
    {
        public DataPointEvent(BucketReference bucket, DataPoint dataPoint)
        {
            Bucket = bucket;
            DataPoint = dataPoint;
        }

        public BucketReference Bucket { get; private set; }

        public DataPoint DataPoint { get; private set; }
    }
}