using Statr.Configuration;

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
        /// <param name="retention">The retention for this route</param>
        /// <returns>A metric route</returns>
        IMetricRoute Build(Retention retention);
    }
}