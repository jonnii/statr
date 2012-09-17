namespace Statr.Routing
{
    /// <summary>
    /// The metric router is responsible for taking a metric and routing it to
    /// the correct metric route.
    /// </summary>
    public interface IMetricRouter
    {
        /// <summary>
        /// The number of metrics processed so far
        /// </summary>
        ulong NumProcessedMetrics { get; }

        /// <summary>
        /// Route a metric
        /// </summary>
        /// <param name="metric">The metric to route</param>
        void Route(Metric metric);
    }
}