using System.Collections.Generic;

namespace Statr.Server.Storage
{
    public interface IDataPointCache
    {
        IEnumerable<DataPoint> Get(BucketReference bucket);
    }
}