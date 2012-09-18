using System.Collections.Generic;

namespace Statr.Server.Storage
{
    /// <summary>
    /// The storage engine is responsible for writing/reading and managing
    /// persisted data points
    /// </summary>
    public interface IStorageEngine
    {
        /// <summary>
        /// Lists the buckets that have already been persisted
        /// </summary>
        IEnumerable<BucketReference> ListBuckets(string tree);

        /// <summary>
        /// Gets a data point writer for a tree and bucket reference
        /// </summary>
        IDataPointWriter GetWriter(string @namespace, BucketReference bucketReference);
    }
}