using System.Collections.Generic;
using System.Web.Http;
using Statr.Server.Storage;

namespace Statr.Server.Management.Controllers
{
    public class BucketsController : ApiController
    {
        private readonly IBucketRepository bucketRepository;

        public BucketsController(IBucketRepository bucketRepository)
        {
            this.bucketRepository = bucketRepository;
        }

        public IEnumerable<Bucket> Get()
        {
            return bucketRepository.List();
        }
    }
}
