using System.Collections.Generic;

namespace Statr.Server.Storage.Engine
{
    public interface IStorageNode
    {
        string FilePath { get; }

        void Store(IEnumerable<DataPoint> dataPoints);

        IStorageSlice CreateSlice(long startTime, int timeStep);
    }
}