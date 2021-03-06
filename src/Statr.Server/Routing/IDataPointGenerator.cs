using System;

namespace Statr.Server.Routing
{
    /// <summary>
    /// A data point generator creates data points, they can be
    /// registered with the data point source.
    /// </summary>
    public interface IDataPointGenerator
    {
        /// <summary>
        /// The data points that are generated by this data point generator
        /// </summary>
        IObservable<DataPointEvent> DataPoints { get; }
    }
}