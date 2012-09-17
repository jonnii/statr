using System.Collections.Generic;

namespace Statr.Server.Storage
{
    public interface IDataPointRepository
    {
        IEnumerable<DataPoint> Get(BucketReference bucket);
    }

}