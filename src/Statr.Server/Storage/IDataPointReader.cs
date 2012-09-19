using System.Collections.Generic;

namespace Statr.Server.Storage
{
    public interface IDataPointReader
    {
        IEnumerable<DataPoint> Read();
    }
}