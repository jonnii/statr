using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;

namespace Statr.Routing
{
    public class MetricRouteRegistry : IMetricRouteRegistry
    {
        private readonly List<RouteDefinition> routeDefinitions =
            new List<RouteDefinition>();

        private readonly ConcurrentDictionary<string, IMetricRoute[]> registeredRoutes =
            new ConcurrentDictionary<string, IMetricRoute[]>();

        public MetricRouteRegistry()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int NumRoutes
        {
            get { return registeredRoutes.Count; }
        }

        public IEnumerable<IMetricRoute> GetRoutes(Metric metric)
        {
            return registeredRoutes.GetOrAdd(metric.Name, BuildRoutes);
        }

        public void RegisterRoute(RouteDefinition routeDefinition)
        {
            Logger.DebugFormat("Registering route: {0}", routeDefinition);
            routeDefinitions.Add(routeDefinition);
        }

        public IMetricRoute[] BuildRoutes(string metricName)
        {
            Logger.DebugFormat("Building routes for: {0}", metricName);

            var matchingDefinitions = routeDefinitions.First(d => d.AppliesTo(metricName));

            return new IMetricRoute[]
            {
                new MetricRoute(metricName)
            };
        }
    }
}