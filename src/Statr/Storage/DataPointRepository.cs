using System.Collections.Generic;
using Statr.Routing;

namespace Statr.Storage
{
    public class DataPointRepository : IDataPointRepository
    {
        private readonly IDataPointCache dataPointCache;

        public DataPointRepository(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;
        }

        public IEnumerable<DataPoint> Get(Bucket bucket)
        {
            return dataPointCache.Get(bucket);
        }
    }
}