using System.Net;
using System.Net.Http;
using System.Web.Http;
using Statr.Api;
using Statr.Api.Models;

namespace Statr.Web.Controllers.api
{
    public class ConfigsController : ApiController
    {
        private readonly IStatrApi api;

        public ConfigsController(IStatrApi api)
        {
            this.api = api;
        }

        public Config Get(string id)
        {
            if (id == "current")
            {
                return api.Config();
            }

            throw new HttpResponseException(
                new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}