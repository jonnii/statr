namespace Statr.Routing
{
    /// <summary>
    /// The metric route factory is responsible for building metric routes
    /// </summary>
    public interface IMetricRouteFactory
    {
        /// <summary>
        /// Builds a metric route
        /// </summary>
        /// <param name="metricName">The name of the metric</param>
        /// <param name="frequencyInSeconds">The frequency in seconds that this metric route will publish aggregated metrics</param>
        /// <returns>A metric route</returns>
        IMetricRoute Build(string metricName, int frequencyInSeconds);
    }
}