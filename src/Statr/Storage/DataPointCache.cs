using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Statr.Routing;

namespace Statr.Storage
{
    public class DataPointCache : IDataPointCache, IDataPointSubscriber
    {
        private readonly ConcurrentDictionary<Bucket, DataPointCollection> dataPoints =
            new ConcurrentDictionary<Bucket, DataPointCollection>();

        public DataPointCache()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Push(Bucket bucket, DataPoint dataPoint)
        {
            var pushedDataPoint = dataPoint;

            Logger.InfoFormat("Received data point: {0}", pushedDataPoint);

            dataPoints.AddOrUpdate(
                bucket,
                key => CreateDataPointCacheEntry(key, dataPoint),
                (key, list) => list.Add(pushedDataPoint));
        }

        public DataPointCollection CreateDataPointCacheEntry(Bucket key, DataPoint point)
        {
            Logger.DebugFormat("Creating data point cache entry for {0}", key);

            return new DataPointCollection { point };
        }

        public IEnumerable<Bucket> GetBuckets()
        {
            return dataPoints.Keys;
        }

        public IEnumerable<DataPoint> Get(Bucket bucket)
        {
            DataPointCollection collection;

            return dataPoints.TryGetValue(bucket, out collection)
                       ? collection
                       : Enumerable.Empty<DataPoint>();
        }
    }
}
