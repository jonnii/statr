using System.Collections.Concurrent;
using System.Collections.Generic;
using Castle.Core.Logging;

namespace Statr.Storage
{
    public class BucketRepository : IBucketRepository
    {
        private readonly ConcurrentDictionary<BucketReference, Bucket> buckets =
            new ConcurrentDictionary<BucketReference, Bucket>();

        public BucketRepository()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<Bucket> List()
        {
            return buckets.Values;
        }

        public Bucket Get(BucketReference bucketReference)
        {
            return buckets.GetOrAdd(bucketReference, BuildBucket);
        }

        private Bucket BuildBucket(BucketReference bucketReference)
        {
            Logger.DebugFormat("Building new bucket for {0}", bucketReference);

            return new Bucket(bucketReference.Name, bucketReference.MetricType);
        }
    }
}