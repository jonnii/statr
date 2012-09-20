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

        public IEnumerable<DataPoint> Get(string metricType, string id)
        {
            var parsed = MetricTypeParser.Parse(metricType);
            return dataPointCache.Get(new BucketReference(parsed, id));
        }
    }
}
