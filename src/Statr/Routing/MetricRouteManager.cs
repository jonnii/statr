using System.Collections.Concurrent;
using System.Linq;
using Castle.Core.Logging;
using Statr.Configuration;
using Statr.Storage;

namespace Statr.Routing
{
    public class MetricRouteManager : IMetricRouteManager
    {
        private readonly IMetricRouteFactory metricRouteFactory;

        private readonly IBucketRepository bucketRepository;

        private readonly IConfigRepository configRepository;

        private readonly IDataPointSubscriber[] dataPointSubscribers;

        private readonly ConcurrentDictionary<BucketReference, IMetricRoute> registeredRoutes =
            new ConcurrentDictionary<BucketReference, IMetricRoute>();

        public MetricRouteManager(
            IMetricRouteFactory metricRouteFactory,
            IBucketRepository bucketRepository,
            IConfigRepository configRepository,
            IDataPointSubscriber[] dataPointSubscribers)
        {
            this.metricRouteFactory = metricRouteFactory;
            this.bucketRepository = bucketRepository;
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

        public IMetricRoute BuildRoute(BucketReference bucketReference)
        {
            Logger.InfoFormat("Building routes for: {0}", bucketReference);

            var configuration = configRepository.GetConfiguration();

            var highestFrequencyRetention = configuration.GetRetentions(bucketReference.Name)
                .OrderBy(r => r.Frequency)
                .First();

            return BuildRoute(bucketReference, highestFrequencyRetention);
        }

        public IMetricRoute BuildRoute(BucketReference bucketReference, Retention retention)
        {
            var bucket = bucketRepository.Get(bucketReference);
            var strategy = bucket.BuildAggregationStrategy();

            var route = metricRouteFactory.Build(bucketReference, retention.Frequency, strategy);

            Logger.InfoFormat(
                " => Building route: {0} ({1}@{2} w/ {3})",
                bucketReference.Name,
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