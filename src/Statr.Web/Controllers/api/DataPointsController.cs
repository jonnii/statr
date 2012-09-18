using System.Collections.Generic;
using System.Web.Http;
using Statr.Api;
using Statr.Api.Models;

namespace Statr.Web.Controllers.api
{
    public class DataPointsController : ApiController
    {
        private readonly IStatrApi api;

        public DataPointsController(IStatrApi api)
        {
            this.api = api;
        }

        public IEnumerable<DataPoint> Get(string id, string metricType)
        {
            return api.DataPoints(id, metricType);
        }
    }
}