using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Statr.Server.Routing;

namespace Statr.Server.Storage
{
    public class DataPointCache : IDataPointCache, IDisposable
    {
        private readonly IDataPointStream dataPointStream;

        private readonly ConcurrentDictionary<BucketReference, List<DataPoint>> dataPoints =
            new ConcurrentDictionary<BucketReference, List<DataPoint>>();

        private IDisposable subscription;

        public DataPointCache(IDataPointStream dataPointStream)
        {
            this.dataPointStream = dataPointStream;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            Logger.DebugFormat("Starting data point cache, subscribing to data point stream");

            subscription = dataPointStream.DataPoints.Subscribe(Push);
        }

        public void Push(DataPointEvent dataPointEvent)
        {
            var bucket = dataPointEvent.Bucket;
            var dataPoint = dataPointEvent.DataPoint;

            Logger.InfoFormat("Received data point: {0}", dataPoint);

            dataPoints.AddOrUpdate(
                bucket,
                key => CreateDataPointCacheEntry(key, dataPoint),
                (key, list) =>
                {
                    list.Add(dataPoint);
                    return list;
                });
        }

        public List<DataPoint> CreateDataPointCacheEntry(BucketReference key, DataPoint point)
        {
            Logger.DebugFormat("Creating data point cache entry for {0}", key);

            return new List<DataPoint> { point };
        }

        public IEnumerable<DataPoint> Get(BucketReference bucket)
        {
            List<DataPoint> collection;

            return dataPoints.TryGetValue(bucket, out collection)
                       ? collection
                       : Enumerable.Empty<DataPoint>();
        }

        public void Dispose()
        {
            Logger.Info("Disposing of DataPointCache");

            if (subscription != null)
            {
                subscription.Dispose();
            }
        }
    }
}
