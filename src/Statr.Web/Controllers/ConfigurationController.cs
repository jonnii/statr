using System.Web.Mvc;
using SpeakEasy;
using Statr.Configuration;

namespace Statr.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IHttpClient api;

        public ConfigurationController(IHttpClient api)
        {
            this.api = api;
        }

        public ActionResult Index()
        {
            var config = api.Get("config").OnOk().As<Config>();

            return View(config);
        }
    }
}
