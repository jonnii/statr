using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;
using Castle.Core.Logging;
using Castle.Windsor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Statr.Server.Management.Windsor;

namespace Statr.Server.Management
{
    public class SelfHostHttpConfigurationFactory
    {
        private readonly IWindsorContainer container;

        public SelfHostHttpConfigurationFactory(IWindsorContainer container)
        {
            this.container = container;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int Port { get; set; }

        public HttpSelfHostConfiguration Create()
        {
            if (Port == 0)
            {
                throw new StatrException(
                    "Cannot start the self host http configuration factory without the port being set, was it set in the windsor config?");
            }

            var listeningOn = string.Format("http://127.0.0.1:{0}/", Port);

            Logger.InfoFormat("Starting management app host on: {0}", listeningOn);
            var httpConfiguration = new HttpSelfHostConfiguration(listeningOn);

            ConfigureRoutes(httpConfiguration.Routes);
            ConfigureWebApi(httpConfiguration);

            return httpConfiguration;
        }

        private void ConfigureRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute("dataPoints", "DataPoints/{metricType}/{id}", new { controller = "DataPoints" });
            routes.MapHttpRoute("default", "{controller}/{id}", new { id = RouteParameter.Optional });
        }

        public void ConfigureWebApi(HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new WindsorWebApiDependencyResolver(container);

            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            ConfigureFormatters(configuration);
            ConfigureFilters(configuration);
        }

        private void ConfigureFormatters(HttpConfiguration configuration)
        {
            var defaultFormatters = configuration.Formatters
                .Where(f => f.SupportedMediaTypes
                    .Any(m => m.MediaType.ToString(CultureInfo.InvariantCulture) == "application/xml"
                                || m.MediaType.ToString(CultureInfo.InvariantCulture) == "text/xml"))
                .ToList();

            foreach (var match in defaultFormatters)
            {
                configuration.Formatters.Remove(match);
            }

            var settings = configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.Converters.Add(new IsoDateTimeConverter());
            settings.Converters.Add(new StringEnumConverter());
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        private void ConfigureFilters(HttpConfiguration configuration)
        {
            var filters = container.ResolveAll<IFilter>();
            foreach (var filter in filters)
            {
                configuration.Filters.Add(filter);
            }
        }
    }
}