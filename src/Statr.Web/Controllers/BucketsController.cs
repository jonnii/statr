using System.Web.Mvc;
using Statr.Api;

namespace Statr.Web.Controllers
{
    public class BucketsController : Controller
    {
        private readonly IStatrApi api;

        public BucketsController(IStatrApi api)
        {
            this.api = api;
        }

        public ActionResult Index()
        {
            var buckets = api.GetBuckets();

            return View(buckets);
        }
    }
}