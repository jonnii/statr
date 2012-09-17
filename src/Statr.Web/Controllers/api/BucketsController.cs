using System.Web.Http;
using Statr.Api;

namespace Statr.Web.Controllers.api
{
    public class BucketsController : ApiController
    {
        private readonly IStatrApi api;

        public BucketsController(IStatrApi api)
        {
            this.api = api;
        }

        //public IEnumerable<Bucket> Get()
        //{
        //    return api.GetBuckets();
        //}
    }
}