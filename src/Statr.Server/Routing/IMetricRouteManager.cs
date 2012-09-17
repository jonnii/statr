using System.Collections.Generic;

namespace Statr.Routing
{
    public interface IMetricRouteManager
    {
        IEnumerable<IMetricRoute> Routes { get; }

        IMetricRoute GetRoute(Metric metric);

        void FlushAll();
    }
}