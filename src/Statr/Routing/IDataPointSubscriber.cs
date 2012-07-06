namespace Statr.Routing
{
    public interface IDataPointSubscriber
    {
        void Push(Bucket bucket, DataPoint dataPoint);
    }
}
