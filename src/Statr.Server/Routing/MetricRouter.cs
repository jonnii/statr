using Castle.Core.Logging;

namespace Statr.Server.Routing
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

            var routes = metricRouteManager.GetRoute(metric);

            foreach (var route in routes)
            {
                route.Push(metric);
            }

            ++NumProcessedMetrics;
        }
    }
}