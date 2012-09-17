using System.Collections.Generic;

namespace Statr.Server.Storage
{
    public class DataPointRepository : IDataPointRepository
    {
        private readonly IDataPointCache dataPointCache;

        public DataPointRepository(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;
        }

        public IEnumerable<DataPoint> Get(BucketReference bucket)
        {
            return dataPointCache.Get(bucket);
        }
    }
}