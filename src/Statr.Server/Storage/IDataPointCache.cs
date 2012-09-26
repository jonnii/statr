using System.Collections.Generic;

namespace Statr.Server.Storage
{
    public interface IDataPointCache
    {
        IEnumerable<DataPoint> GetAll(BucketReference bucket);

        IEnumerable<DataPoint> GetRecent(BucketReference bucket, int limit);
    }
}