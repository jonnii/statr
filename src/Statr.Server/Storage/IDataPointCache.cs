using System.Collections.Generic;

namespace Statr.Storage
{
    public interface IDataPointCache
    {
        IEnumerable<DataPoint> Get(BucketReference bucket);
    }
}