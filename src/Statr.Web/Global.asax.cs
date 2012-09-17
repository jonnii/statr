using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Statr.Web.App_Start;
using Statr.Web.Windsor;

namespace Statr.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var defaultFormatters = GlobalConfiguration.Configuration.Formatters
                .Where(f => f.SupportedMediaTypes
                    .Any(m => m.MediaType.ToString(CultureInfo.InvariantCulture) == "application/xml"
                                || m.MediaType.ToString(CultureInfo.InvariantCulture) == "text/xml"))
                .ToList();

            foreach (var match in defaultFormatters)
            {
                GlobalConfiguration.Configuration.Formatters.Remove(match);
            }

            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.Converters.Add(new IsoDateTimeConverter());
            settings.Converters.Add(new StringEnumConverter());
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var application = new WebApplication(GlobalConfiguration.Configuration);
            application.Initialize();
            var container = application.Container;

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
        }
    }
}