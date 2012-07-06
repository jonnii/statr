namespace Statr.Routing
{
    public interface IMetricRouteManager
    {
        IMetricRoute GetRoute(Metric metric);

        void FlushAll();
    }
}