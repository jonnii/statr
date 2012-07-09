using System;
using System.Collections.Generic;

namespace Statr.Storage
{
    /// <summary>
    /// A storage strategy is used to transform the data point stream into
    /// something that can be written to the storage engine
    /// </summary>
    public interface IStorageStrategy
    {
        /// <summary>
        /// Applies the storage strategy to the given data points
        /// </summary>
        /// <param name="dataPoints">The data points to transform</param>
        /// <returns>The transformed data points</returns>
        IObservable<IEnumerable<DataPoint>> Apply(IObservable<DataPoint> dataPoints);
    }
}