using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Castle.Core.Logging;
using Statr.Extensions;

namespace Statr.Routing
{
    public class MetricRoute : IMetricRoute
    {
        private readonly IAggregationStrategy aggregationStrategy;

        private readonly Subject<DataPointEvent> dataPoints = new Subject<DataPointEvent>();

        private IObservable<AggregatedMetric> aggregatedWindows;

        private IDisposable subscription;

        public MetricRoute(
            BucketReference bucketReference,
            int frequencyInSeconds,
            IAggregationStrategy aggregationStrategy)
        {
            this.aggregationStrategy = aggregationStrategy;

            Bucket = bucketReference;
            FrequencyInSeconds = frequencyInSeconds;

            Logger = NullLogger.Instance;
        }

        public event EventHandler<MetricEventArgs> MetricReceived;

        public IObservable<DataPointEvent> DataPoints
        {
            get { return dataPoints; }
        }

        public ILogger Logger { get; set; }

        public BucketReference Bucket { get; private set; }

        public int FrequencyInSeconds { get; private set; }

        public ulong NumProcessedMetrics { get; private set; }

        public ulong NumPublishedDataPoints { get; private set; }

        public void Start()
        {
            Logger.InfoFormat("Starting route {0}", Bucket);

            var observable = Observable.FromEventPattern<EventHandler<MetricEventArgs>, MetricEventArgs>(
                h => MetricReceived += h,
                h => MetricReceived -= h);

            // create windows for the frequency of this route
            var windows = observable.Window(TimeSpan.FromSeconds(FrequencyInSeconds));

            // aggregate all the metrics in the windows
            aggregatedWindows = windows.SelectMany(
                window => window.Aggregate(new AggregatedMetric(), AggregateMetrics));

            // subscribe to the aggregated metrics
            subscription = aggregatedWindows.Subscribe(OnMetricsAggregated);
        }

        public AggregatedMetric AggregateMetrics(AggregatedMetric original, EventPattern<MetricEventArgs> newMetric)
        {
            return aggregationStrategy.Aggregate(original, newMetric.EventArgs.Metric);
        }

        public void OnMetricsAggregated(AggregatedMetric aggregatedMetrics)
        {
            if (aggregatedMetrics == null || !aggregatedMetrics.HasMetrics)
            {
                return;
            }

            dataPoints.OnNext(new DataPointEvent(Bucket, aggregatedMetrics.ToDataPoint()));

            ++NumPublishedDataPoints;
        }

        public void Push(Metric metric)
        {
            MetricReceived.Raise(this, new MetricEventArgs(metric));
            ++NumProcessedMetrics;
        }

        public void Flush()
        {
            OnMetricsAggregated(aggregationStrategy.Current);
        }

        public void Dispose()
        {
            Logger.InfoFormat("Disposing of metric route: {0}", Bucket);

            if (subscription == null)
            {
                return;
            }

            subscription.Dispose();

            Flush();
        }
    }
}