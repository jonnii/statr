namespace Statr.Routing
{
    public interface IDataPointSubscriber
    {
        void Push(BucketReference bucket, DataPoint dataPoint);
    }
}
