using System.Collections.Generic;

namespace Statr.Server.Storage
{
    /// <summary>
    /// The bucket repository is responsible for managing the available buckets
    /// </summary>
    public interface IBucketRepository
    {
        /// <summary>
        /// Lists the available buckets
        /// </summary>
        /// <returns>The buckets available</returns>
        IEnumerable<Bucket> List();

        /// <summary>
        /// Gets a bucket by bucket reference
        /// </summary>
        /// <param name="bucketReference">The bucket reference to get</param>
        /// <returns>A bucket</returns>
        Bucket Get(BucketReference bucketReference);
    }
}
