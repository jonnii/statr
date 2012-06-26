using System;
using System.Reactive;
using System.Reactive.Linq;
using Statr.Extensions;

namespace Statr.Routing
{
    public class MetricRoute : IMetricRoute
    {
        private IDisposable subscription;

        public MetricRoute(string metricName, int frequencyInSeconds)
        {
            MetricName = metricName;
            FrequencyInSeconds = frequencyInSeconds;
        }

        public event EventHandler<MetricEventArgs> MetricReceived;

        public event EventHandler<DataPointEventArgs> DataPointGenerated;

        public string MetricName { get; private set; }

        public int FrequencyInSeconds { get; private set; }

        public ulong NumProcessedMetrics { get; private set; }

        public ulong NumPublishedDataPoints { get; private set; }

        public void Start()
        {
            var observable = Observable.FromEventPattern<EventHandler<MetricEventArgs>, MetricEventArgs>(
                h => MetricReceived += h,
                h => MetricReceived -= h);

            // create windows for the frequency of this route
            var windows = observable.Window(TimeSpan.FromSeconds(FrequencyInSeconds));

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