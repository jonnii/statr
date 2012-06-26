using System.Collections.Generic;
using Castle.Core.Logging;

namespace Statr.Routing
{
    public class MetricRouter : IMetricRouter
    {
        private readonly IMetricRouteManager metricRouteManager;

        public MetricRouter(IMetricRouteManager metricRouteManager)
        {
            this.metricRouteManager = metricRouteManager;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public ulong NumProcessedMetrics { get; private set; }

        public void Route(Metric metric)
        {
            Logger.DebugFormat("Routing {0}", metric);

            var matchingRoutes = GetMetricRoutes(metric);

            foreach (var route in matchingRoutes)
            {
                route.Push(metric);
            }

            ++NumProcessedMetrics;
        }

        public IEnumerable<IMetricRoute> GetMetricRoutes(Metric metric)
        {
            return metricRouteManager.GetRoutes(metric);
        }
    }
}