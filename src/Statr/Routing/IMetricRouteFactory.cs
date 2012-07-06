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
        /// <param name="bucketReference">The bucket this metric route leads to</param>
        /// <param name="frequencyInSeconds">The frequency of this metric route</param>
        /// <param name="aggregationStrategy">The aggregation strategy for this metric route</param>
        /// <returns>A metric route</returns>
        IMetricRoute Build(BucketReference bucketReference, int frequencyInSeconds, IAggregationStrategy aggregationStrategy);
    }
}