using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using Castle.Core.Logging;

namespace Statr.Routing
{
    public class MetricRouter : IMetricRouter
    {
        private readonly ConcurrentDictionary<RouteDefinition, IMetricRoute> routes =
            new ConcurrentDictionary<RouteDefinition, IMetricRoute>();

        public MetricRouter()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Route(Metric metric)
        {
            Logger.DebugFormat("Routing {0}", metric);

            var matchingRoutes = routes.Keys.Where(
                k => Regex.IsMatch(metric.Name, k.Pattern));

            foreach (var matchingRoute in matchingRoutes)
            {
                IMetricRoute route;
                if (routes.TryGetValue(matchingRoute, out route))
                {
                    route.NotifyMetric(metric);
                }
            }
        }

        public IMetricRoute RegisterRoute(RouteDefinition routeDefinition)
        {
            var subscription = new MetricRoute();
            routes.TryAdd(routeDefinition, subscription);
            return subscription;
        }
    }
}