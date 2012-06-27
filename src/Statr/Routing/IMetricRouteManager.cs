namespace Statr.Routing
{
    public interface IMetricRouteManager
    {
        IMetricRoute GetRoutes(Metric metric);
    }
}