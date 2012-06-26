using System;
using System.Reactive;
using System.Reactive.Linq;
using Castle.Core.Logging;
using Statr.Configuration;
using Statr.Extensions;

namespace Statr.Routing
{
    public class MetricRoute : IMetricRoute
    {
        private IDisposable subscription;

        public MetricRoute(string routeName, Retention retention)
        {
            RouteName = routeName;
            Retention = retention;

            Logger = NullLogger.Instance;
        }

        public event EventHandler<MetricEventArgs> MetricReceived;

        public event EventHandler<DataPointEventArgs> DataPointGenerated;

        public ILogger Logger { get; set; }

        public string RouteName { get; private set; }

        public Retention Retention { get; private set; }

        public ulong NumProcessedMetrics { get; private set; }

        public ulong NumPublishedDataPoints { get; private set; }

        public void Start()
        {
            Logger.DebugFormat("Starting route", RouteName);

            var observable = Observable.FromEventPattern<EventHandler<MetricEventArgs>, MetricEventArgs>(
                h => MetricReceived += h,
                h => MetricReceived -= h);

            // create windows for the frequency of this route
            var windows = observable.Window(TimeSpan.FromSeconds(Retention.Frequency));

            // aggregate all the metrics in the windows
            var aggregatedWindows = windows.SelectMany(
                window => window.Aggregate(new AggregatedMetric(), AggregateMetrics));

            // subscribe to the aggregated metrics
            subscription = aggregatedWindows.Subscribe(OnMetricsAggregated);
        }

        public AggregatedMetric AggregateMetrics(AggregatedMetric original, EventPattern<MetricEventArgs> newMetric)
        {
            return AggregateMetrics(original, newMetric.EventArgs.Metric);
        }

        public AggregatedMetric AggregateMetrics(AggregatedMetric original, Metric metric)
        {
            var countMetric = (CountMetric)metric;

            return new AggregatedMetric
            {
                LastValue = countMetric.Amount,
                NumMetrics = ++original.NumMetrics,
                Value = original.Value + countMetric.Amount
            };
        }

        public void OnMetricsAggregated(AggregatedMetric aggregatedMetrics)
        {
            if (!aggregatedMetrics.HasMetrics)
            {
                return;
            }

            DataPointGenerated.Raise(this, new DataPointEventArgs(aggregatedMetrics.ToDataPoint()));
            ++NumPublishedDataPoints;
        }

        public void Push(Metric metric)
        {
            MetricReceived.Raise(this, new MetricEventArgs(metric));
            ++NumProcessedMetrics;
        }

        public void Dispose()
        {
            if (subscription != null)
            {
                subscription.Dispose();
            }
        }
    }
}