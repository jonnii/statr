using System;

namespace Statr.Routing
{
    public interface IMetricRoute
    {
        event EventHandler<MetricEventArgs> MetricReceived;

        void NotifyMetric(Metric metric);
    }
}