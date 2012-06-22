using System;

namespace Statr.Routing
{
    public class MetricRoute : IMetricRoute
    {
        public event EventHandler<MetricEventArgs> MetricReceived;

        public void NotifyMetric(Metric metric)
        {
            var handler = MetricReceived;
            if (handler != null)
            {
                handler(this, new MetricEventArgs(metric));
            }
        }
    }
}