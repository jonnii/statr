using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Statr.Routing;

namespace Statr.Storage
{
    public class DataPointCache : IDataPointCache, IDataPointSubscriber
    {
        private readonly ConcurrentDictionary<BucketReference, DataPointCollection> dataPoints =
            new ConcurrentDictionary<BucketReference, DataPointCollection>();

        public DataPointCache()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Push(BucketReference bucket, DataPoint dataPoint)
        {
            var pushedDataPoint = dataPoint;

            Logger.InfoFormat("Received data point: {0}", pushedDataPoint);

            dataPoints.AddOrUpdate(
                bucket,
                key => CreateDataPointCacheEntry(key, dataPoint),
                (key, list) => list.Add(pushedDataPoint));
        }

        public DataPointCollection CreateDataPointCacheEntry(BucketReference key, DataPoint point)
        {
            Logger.DebugFormat("Creating data point cache entry for {0}", key);

            return new DataPointCollection { point };
        }

        public IEnumerable<DataPoint> Get(BucketReference bucket)
        {
            DataPointCollection collection;

            return dataPoints.TryGetValue(bucket, out collection)
                       ? collection
                       : Enumerable.Empty<DataPoint>();
        }
    }
}
