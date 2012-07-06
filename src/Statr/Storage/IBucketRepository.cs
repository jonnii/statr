using System.Collections.Generic;

namespace Statr.Storage
{
    public interface IBucketRepository
    {
        IEnumerable<Bucket> List();

        Bucket Get(BucketReference bucketReference);
    }
}
