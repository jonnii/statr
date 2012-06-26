using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Statr.Configuration;

namespace Statr.Routing
{
    public class MetricRouteRegistry : IMetricRouteRegistry
    {
        private readonly IMetricRouteFactory metricRouteFactory;

        private readonly IConfigRepository configRepository;

        private readonly ConcurrentDictionary<string, IMetricRoute[]> registeredRoutes =
            new ConcurrentDictionary<string, IMetricRoute[]>();

        public MetricRouteRegistry(
            IMetricRouteFactory metricRouteFactory,
            IConfigRepository configRepository)
        {
            this.metricRouteFactory = metricRouteFactory;
            this.configRepository = configRepository;
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

        public IMetricRoute[] BuildRoutes(string metricName)
        {
            Logger.DebugFormat("Building routes for: {0}", metricName);

            var configuration = configRepository.GetConfiguration();

            var entries = configuration.GetRouteDefinitions(metricName);

            return entries.Select(metricRouteFactory.Build).ToArray();
        }
    }
}