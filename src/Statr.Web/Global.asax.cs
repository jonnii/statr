﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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

            var application = new WebApplication();
            application.Initialize();
            var container = application.Container;

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
        }
    }
}