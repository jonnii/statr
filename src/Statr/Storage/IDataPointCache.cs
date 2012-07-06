using System.Collections.Generic;
using Statr.Routing;

namespace Statr.Storage
{
    public interface IDataPointCache
    {
        IEnumerable<Bucket> GetBuckets();

        IEnumerable<DataPoint> Get(Bucket bucket);
    }
}