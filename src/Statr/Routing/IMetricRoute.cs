using System;

namespace Statr.Routing
{
    public interface IMetricRoute : IDisposable
    {
        string Key { get; }

        void Push(Metric metric);
    }
}