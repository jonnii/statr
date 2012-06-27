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

            var mathchingRoute = GetMetricRoutes(metric);

            mathchingRoute.Push(metric);

            ++NumProcessedMetrics;
        }

        public IMetricRoute GetMetricRoutes(Metric metric)
        {
            return metricRouteManager.GetRoutes(metric);
        }
    }
}