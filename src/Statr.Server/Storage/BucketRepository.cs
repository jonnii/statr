using System.Collections.Concurrent;
using System.Collections.Generic;
using Castle.Core.Logging;

namespace Statr.Server.Storage
{
    public class BucketRepository : IBucketRepository
    {
        private readonly IStorageEngine storageEngine;

        private readonly ConcurrentDictionary<BucketReference, Bucket> buckets =
            new ConcurrentDictionary<BucketReference, Bucket>();

        public BucketRepository(IStorageEngine storageEngine)
        {
            this.storageEngine = storageEngine;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void FetchInitialBucketList()
        {
            Logger.Debug("Fetching initial bucket list");

            var persisted = storageEngine.ListBuckets();
            foreach (var reference in persisted)
            {
                Get(reference);
            }
        }

        public IEnumerable<Bucket> List()
        {
            return buckets.Values;
        }

        public bool Exists(BucketReference bucketReference)
        {
            return buckets.ContainsKey(bucketReference);
        }

        public Bucket Get(BucketReference bucketReference)
        {
            return buckets.GetOrAdd(bucketReference, BuildBucket);
        }

        private Bucket BuildBucket(BucketReference bucketReference)
        {
            Logger.DebugFormat("Building new bucket for {0}", bucketReference);

            return new Bucket(
                bucketReference.Name,
                bucketReference.MetricType);
        }
    }
}