using System.Collections.Generic;
using Castle.Core.Logging;

namespace Statr.Routing
{
    public class MetricRouter : IMetricRouter
    {
        private readonly IMetricRouteRegistry metricRouteRegistry;

        public MetricRouter(IMetricRouteRegistry metricRouteRegistry)
        {
            this.metricRouteRegistry = metricRouteRegistry;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Route(Metric metric)
        {
            Logger.DebugFormat("Routing {0}", metric);

            var matchingRoutes = GetMetricRoutes(metric);

            foreach (var route in matchingRoutes)
            {
                route.Push(metric);
            }
        }

        public IEnumerable<IMetricRoute> GetMetricRoutes(Metric metric)
        {
            return metricRouteRegistry.GetRoutes(metric);
        }
    }
}