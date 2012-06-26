using System.Collections.Generic;

namespace Statr.Routing
{
    public interface IMetricRouteRegistry
    {
        IEnumerable<IMetricRoute> GetRoutes(Metric metric);
    }
}