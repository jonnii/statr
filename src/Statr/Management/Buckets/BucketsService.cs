using ServiceStack.ServiceInterface;
using Statr.Storage;

namespace Statr.Management.Buckets
{
    public class BucketsService : RestServiceBase<BucketsRequest>
    {
        private readonly IBucketRepository bucketRepository;

        public BucketsService(IBucketRepository bucketRepository)
        {
            this.bucketRepository = bucketRepository;
        }

        public override object OnGet(BucketsRequest request)
        {
            return bucketRepository.List();
        }
    }
}
