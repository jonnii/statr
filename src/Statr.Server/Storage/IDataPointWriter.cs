using System.Collections.Generic;

namespace Statr.Server.Storage
{
    /// <summary>
    /// A data point writer is a handle into a storage engine
    /// into which data points can be written
    /// </summary>
    public interface IDataPointWriter
    {
        /// <summary>
        /// Write the data points
        /// </summary>
        /// <param name="dataPoints">The data points to write</param>
        void Write(IEnumerable<DataPoint> dataPoints);
    }
}