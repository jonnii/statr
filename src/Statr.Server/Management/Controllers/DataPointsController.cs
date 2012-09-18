using System;
using System.Collections.Generic;
using System.Web.Http;
using Statr.Server.Storage;

namespace Statr.Server.Management.Controllers
{
    public class DataPointsController : ApiController
    {
        private readonly IDataPointCache dataPointCache;

        public DataPointsController(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;
        }

        public IEnumerable<DataPoint> Get(string id, string metricType)
        {
            var parsed = ParseMetricType(metricType);
            return dataPointCache.Get(new BucketReference(id, parsed));
        }

        public MetricType ParseMetricType(string metricType)
        {
            switch (metricType)
            {
                case "gauge":
                case "Gauge":
                    return MetricType.Gauge;
                case "count":
                case "Count":
                    return MetricType.Count;
                default:
                    throw new NotSupportedException(
                        string.Format("Could not parse the metric type: {0}", metricType));
            }
        }
    }
}
