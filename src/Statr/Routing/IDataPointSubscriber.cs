namespace Statr.Routing
{
    public interface IDataPointSubscriber
    {
        void Push(RouteKey routeKey, DataPoint dataPoint);
    }
}
