namespace Statr.Routing
{
    public interface IDataPointSubscriber
    {
        void Push(DataPoint dataPoint);
    }
}
