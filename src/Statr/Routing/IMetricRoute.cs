using System;

namespace Statr.Routing
{
    public interface IMetricRoute : IDisposable
    {
        string MetricName { get; }

        void Push(Metric metric);
    }
}