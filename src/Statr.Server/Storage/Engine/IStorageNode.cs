using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public interface IStorageNode
    {
        string FilePath { get; }

        void Write(IEnumerable<DataPoint> dataPoints);

        IEnumerable<DataPoint> Read();

        IEnumerable<IStorageSlice> GetSlices();
    }
}