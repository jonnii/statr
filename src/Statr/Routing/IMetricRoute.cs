using System;

namespace Statr.Routing
{
    public interface IMetricRoute : IDisposable
    {
        /// <summary>
        /// The data point generated event is raised when metrics have been 
        /// aggregated.
        /// </summary>
        event EventHandler<DataPointEventArgs> DataPointGenerated;

        /// <summary>
        /// The name of this metric route
        /// </summary>
        string MetricName { get; }

        /// <summary>
        /// Pushes a metric into this metric route
        /// </summary>
        void Push(Metric metric);
    }
}