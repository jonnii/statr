using System.Collections.Generic;

namespace Statr.Routing
{
    public interface IMetricRouteRegistry
    {
        void RegisterRoute(RouteDefinition routeDefinition);

        IEnumerable<IMetricRoute> GetRoutes(Metric metric);
    }
}