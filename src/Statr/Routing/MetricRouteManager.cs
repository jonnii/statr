using System.Collections.Concurrent;
using System.Collections.Generic;
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

        private readonly IDataPointStream dataPointStream;

        private readonly ConcurrentDictionary<BucketReference, IMetricRoute> registeredRoutes =
            new ConcurrentDictionary<BucketReference, IMetricRoute>();

        public MetricRouteManager(
            IMetricRouteFactory metricRouteFactory,
            IBucketRepository bucketRepository,
            IConfigRepository configRepository,
            IDataPointStream dataPointStream)
        {
            this.metricRouteFactory = metricRouteFactory;
            this.bucketRepository = bucketRepository;
            this.configRepository = configRepository;
            this.dataPointStream = dataPointStream;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int NumRoutes
        {
            get { return registeredRoutes.Count; }
        }

        public IEnumerable<IMetricRoute> Routes
        {
            get { return registeredRoutes.Values; }
        }

        public IMetricRoute GetRoute(Metric metric)
        {
            return registeredRoutes.GetOrAdd(metric.ToBucket(), BuildRoute);
        }

        public void FlushAll()
        {
            Logger.DebugFormat("Flushing all routes");

            var routes = Routes;
            registeredRoutes.Clear();

            foreach (var route in routes)
            {
                metricRouteFactory.Release(route);
            }

            registeredRoutes.Clear();
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

            dataPointStream.Register(route);

            route.Start();

            return route;
        }
    }
}