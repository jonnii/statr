using System.Collections.Generic;

namespace Statr.Routing
{
    public interface IMetricRouteManager
    {
        IEnumerable<IMetricRoute> GetRoutes(Metric metric);
    }
}