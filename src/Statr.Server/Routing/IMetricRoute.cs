using System;

namespace Statr.Server.Routing
{
    /// <summary>
    /// A metric route is responsible for taking metrics, processing them
    /// and then ultimately produce data points
    /// </summary>
    public interface IMetricRoute : IDisposable, IDataPointGenerator
    {
        /// <summary>
        /// The bucket for this metric route
        /// </summary>
        BucketReference Bucket { get; }

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