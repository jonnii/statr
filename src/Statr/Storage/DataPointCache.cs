using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Statr.Routing;

namespace Statr.Storage
{
    public class DataPointCache : IDataPointCache, IDataPointSubscriber
    {
        private readonly ConcurrentDictionary<RouteKey, DataPointCollection> dataPoints =
            new ConcurrentDictionary<RouteKey, DataPointCollection>();

        public DataPointCache()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Push(RouteKey routeKey, DataPoint dataPoint)
        {
            var pushedDataPoint = dataPoint;

            Logger.InfoFormat("Received data point: {0}", pushedDataPoint);

            dataPoints.AddOrUpdate(
                routeKey,
                key => CreateDataPointCacheEntry(key, dataPoint),
                (key, list) => list.Add(pushedDataPoint));
        }

        public DataPointCollection CreateDataPointCacheEntry(RouteKey key, DataPoint point)
        {
            Logger.DebugFormat("Creating data point cache entry for {0}", key);

            return new DataPointCollection { point };
        }

        public IEnumerable<DataPoint> Get(RouteKey routeKey)
        {
            DataPointCollection collection;

            return dataPoints.TryGetValue(routeKey, out collection)
                       ? collection
                       : Enumerable.Empty<DataPoint>();
        }
    }
}
