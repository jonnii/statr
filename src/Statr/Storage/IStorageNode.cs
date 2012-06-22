using System.Collections.Generic;

namespace Statr.Storage
{
    public interface IStorageNode
    {
        string FilePath { get; }

        void Store(IEnumerable<DataPoint> points);

        IStorageSlice CreateSlice(long startTime, int timeStep);
    }
}