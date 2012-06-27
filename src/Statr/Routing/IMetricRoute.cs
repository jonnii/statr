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
        /// The route key for this metric route
        /// </summary>
        RouteKey RouteKey { get; }

        /// <summary>
        /// The frequency in seconds for this route
        /// </summary>
        int FrequencyInSeconds { get; }

        /// <summary>
        /// Starts this route, this means the route will start accepting metrics and
        /// will publish data points
        /// </summary>
        void Start();

        /// <summary>
        /// Pushes a metric into this metric route
        /// </summary>
        void Push(Metric metric);
    }
}