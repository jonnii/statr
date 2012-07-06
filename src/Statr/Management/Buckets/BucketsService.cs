using ServiceStack.ServiceInterface;
using Statr.Storage;

namespace Statr.Management.Buckets
{
    public class BucketsService : RestServiceBase<BucketsRequest>
    {
        private readonly IDataPointCache dataPointCache;

        public BucketsService(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;
        }

        public override object OnGet(BucketsRequest request)
        {
            return dataPointCache.GetBuckets();
        }
    }
}
