using System;

namespace Statr
{
    public class MetricEventArgs : EventArgs
    {
        public MetricEventArgs(Metric metric)
        {
            Metric = metric;
        }

        public Metric Metric { get; private set; }
    }
}