using System.Collections.Generic;
using System.Web.Http;
using Castle.Core.Logging;
using Statr.Server.Storage;

namespace Statr.Server.Management.Controllers
{
    public class DataPointsController : ApiController
    {
        private readonly IDataPointCache dataPointCache;

        public DataPointsController(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<DataPoint> Get(string metricType, string id, int limit = 200)
        {
            var parsed = MetricTypeParser.Parse(metricType);
            var bucket = new BucketReference(parsed, id);

            Logger.DebugFormat("Getting Data Points for {0}, [Limit={0}]", bucket, limit);

            return dataPointCache.GetRecent(bucket, limit);
        }
    }
}
