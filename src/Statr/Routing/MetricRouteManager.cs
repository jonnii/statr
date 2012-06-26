using System.Collections.Concurrent;
using System.Collections.Generic;
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

        private readonly ConcurrentDictionary<string, IMetricRoute[]> registeredRoutes =
            new ConcurrentDictionary<string, IMetricRoute[]>();

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

        public IEnumerable<IMetricRoute> GetRoutes(Metric metric)
        {
            return registeredRoutes.GetOrAdd(metric.Name, BuildRoutes);
        }

        public IMetricRoute[] BuildRoutes(string metricName)
        {
            Logger.InfoFormat("Building routes for: {0}", metricName);

            var configuration = configRepository.GetConfiguration();
            var retentions = configuration.GetRetentions(metricName);

            return retentions.Select(
                retention => BuildRoute(metricName, retention)).ToArray();
        }

        public IMetricRoute BuildRoute(string metricName, Retention retention)
        {
            var route = metricRouteFactory.Build(metricName, retention);

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