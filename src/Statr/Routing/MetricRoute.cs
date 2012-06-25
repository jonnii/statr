using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Statr.Routing
{
    public class MetricRoute : IMetricRoute
    {
        private IDisposable subscription;

        public event EventHandler<MetricEventArgs> MetricReceived;

        public event EventHandler<DataPointEventArgs> DataPointGenerated;

        public MetricRoute(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }

        public void Start()
        {
            var observable = Observable.FromEventPattern<EventHandler<MetricEventArgs>, MetricEventArgs>(
              h => MetricReceived += h,
              h => MetricReceived -= h);

            var windows = observable.Window(TimeSpan.FromSeconds(1));

            var aggregatedWindows = windows.SelectMany(
                window => window.Aggregate(new MetricAggregation(), AccumulateMetrics));

            subscription = aggregatedWindows.Subscribe(OnAggregatedWindow);
        }

        private MetricAggregation AccumulateMetrics(MetricAggregation original, EventPattern<MetricEventArgs> newMetric)
        {
            var metric = (CountMetric)newMetric.EventArgs.Metric;
            return new MetricAggregation { LastValue = metric.Amount };
        }

        private void OnAggregatedWindow(MetricAggregation aggregatedWindow)
        {
            var handler = DataPointGenerated;
            if (handler != null)
            {
                handler(this, new DataPointEventArgs(aggregatedWindow.ToDataPoint()));
            }
        }

        public void Push(Metric metric)
        {
            var handler = MetricReceived;
            if (handler != null)
            {
                handler(this, new MetricEventArgs(metric));
            }
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