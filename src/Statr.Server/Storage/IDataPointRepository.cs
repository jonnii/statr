using System.Collections.Generic;

namespace Statr.Storage
{
    public interface IDataPointRepository
    {
        IEnumerable<DataPoint> Get(BucketReference bucket);
    }

}