using System.Web.Mvc;
using Statr.Api;
using Statr.Web.ViewModels;

namespace Statr.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IStatrApi api;

        public ConfigurationController(IStatrApi api)
        {
            this.api = api;
        }

        public ActionResult Index()
        {
            var config = api.GetConfig();

            return View(config);
        }

        public ActionResult Create()
        {
            return View(new CreateConfigurationViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateConfigurationViewModel model)
        {
            return View();
        }
    }
}
