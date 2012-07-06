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

        private readonly IDataPointSubscriber[] dataPointSubscribers;

        private readonly ConcurrentDictionary<Bucket, IMetricRoute> registeredRoutes =
            new ConcurrentDictionary<Bucket, IMetricRoute>();

        public MetricRouteManager(
            IMetricRouteFactory metricRouteFactory,
            IConfigRepository configRepository,
            IDataPointSubscriber[] dataPointSubscribers)
        {
            this.metricRouteFactory = metricRouteFactory;
            this.configRepository = configRepository;
            this.dataPointSubscribers = dataPointSubscribers;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int NumRoutes
        {
            get { return registeredRoutes.Count; }
        }

        public IMetricRoute GetRoute(Metric metric)
        {
            return registeredRoutes.GetOrAdd(metric.ToBucket(), BuildRoute);
        }

        public void FlushAll()
        {
            var routes = registeredRoutes.Values;

            foreach (var route in routes)
            {
                route.Flush();
            }
        }

        public IMetricRoute BuildRoute(Bucket bucket)
        {
            Logger.InfoFormat("Building routes for: {0}", bucket);

            var configuration = configRepository.GetConfiguration();

            var highestFrequencyRetention = configuration.GetRetentions(bucket.Name)
                .OrderBy(r => r.Frequency)
                .First();

            return BuildRoute(bucket, highestFrequencyRetention);
        }

        public IMetricRoute BuildRoute(Bucket bucket, Retention retention)
        {
            var strategy = new AccumulateAggregationStrategy();

            var route = metricRouteFactory.Build(bucket, retention.Frequency, strategy);

            Logger.InfoFormat(
                " => Building route: {0} ({1}@{2} w/ {3})",
                bucket.Name,
                retention.Frequency,
                retention.History,
                strategy.GetType().Name);

            route.DataPointGenerated += OnRouteOnDataPointGenerated;

            route.Start();

            return route;
        }

        public void OnRouteOnDataPointGenerated(object sender, DataPointEventArgs args)
        {
            Logger.InfoFormat("Notifying data point subscribers");

            foreach (var dataPointSubscriber in dataPointSubscribers)
            {
                dataPointSubscriber.Push(args.Bucket, args.DataPoint);
            }
        }
    }
}