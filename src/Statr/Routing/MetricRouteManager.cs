using System.Collections.Concurrent;
using System.Linq;
using Castle.Core.Logging;
using Statr.Configuration;

namespace Statr.Routing
{
    public class MetricRouteManager : IMetricRouteManager
    {
        private readonly IMetricRouteFactory metricRouteFactory;

        private readonly IConfigRepository configRepository;

        private readonly IDataPointSubscriber dataPointSubscriber;

        private readonly ConcurrentDictionary<RouteKey, IMetricRoute> registeredRoutes =
            new ConcurrentDictionary<RouteKey, IMetricRoute>();

        public MetricRouteManager(
            IMetricRouteFactory metricRouteFactory,
            IConfigRepository configRepository,
            IDataPointSubscriber dataPointSubscriber)
        {
            this.metricRouteFactory = metricRouteFactory;
            this.configRepository = configRepository;
            this.dataPointSubscriber = dataPointSubscriber;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int NumRoutes
        {
            get { return registeredRoutes.Count; }
        }

        public IMetricRoute GetRoute(Metric metric)
        {
            return registeredRoutes.GetOrAdd(metric.ToRouteKey(), BuildRoute);
        }

        public IMetricRoute BuildRoute(RouteKey routeKey)
        {
            Logger.InfoFormat("Building routes for: {0}", routeKey);

            var configuration = configRepository.GetConfiguration();
            var routeKeyName = routeKey.Name;

            var highestFrequencyRetention = configuration.GetRetentions(routeKeyName)
                .OrderBy(r => r.Frequency)
                .First();

            return BuildRoute(routeKeyName, highestFrequencyRetention);
        }

        public IMetricRoute BuildRoute(string metricName, Retention retention)
        {
            var route = metricRouteFactory.Build(metricName, retention.Frequency);

            Logger.InfoFormat(
                " => Building route: {0} ({1}@{2})",
                metricName,
                retention.Frequency,
                retention.History);

            route.DataPointGenerated += OnRouteOnDataPointGenerated;

            route.Start();

            return route;
        }

        public void OnRouteOnDataPointGenerated(object sender, DataPointEventArgs args)
        {
            Logger.InfoFormat("Notifying data point subscribers");

            dataPointSubscriber.Push(args.DataPoint);
        }
    }
}