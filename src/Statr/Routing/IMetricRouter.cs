namespace Statr.Routing
{
    public interface IMetricRouter
    {
        void Route(Metric metric);

        IMetricRoute RegisterRoute(RouteDefinition routeDefinition);
    }
}