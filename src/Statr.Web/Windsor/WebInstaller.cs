﻿using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SignalR.Hubs;
using Statr.Api;
using Statr.Client.Subscriber;
using Statr.Web.Hubs;

namespace Statr.Web.Windsor
{
    public class WebInstaller : IWindsorInstaller
    {
        private readonly HttpConfiguration httpConfiguration;

        public WebInstaller(HttpConfiguration httpConfiguration)
        {
            this.httpConfiguration = httpConfiguration;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly()
                    .BasedOn<IHttpController>().WithServiceSelf().LifestyleTransient(),

                //AllTypes.FromThisAssembly().BasedOn<Hub>().WithServiceSelf(),

                Component.For<DataPoints>(),

                Component.For<IHttpControllerSelector>().ImplementedBy<DefaultHttpControllerSelector>(),
                Component.For<IHttpControllerActivator>().ImplementedBy<DefaultHttpControllerActivator>().LifeStyle.Transient,
                Component.For<IHttpActionSelector>().ImplementedBy<ApiControllerActionSelector>().LifeStyle.Transient,
                Component.For<IActionValueBinder>().ImplementedBy<DefaultActionValueBinder>().LifeStyle.Transient,
                Component.For<IHttpActionInvoker>().ImplementedBy<ApiControllerActionInvoker>().LifeStyle.Transient,

                // http configuration needs to be registered with the container otherwise we can't
                // create controllers
                Component.For<HttpConfiguration>().Instance(httpConfiguration),

                Component.For<IStatrApi>().ImplementedBy<StatrApi>(),
                Component.For<IDataPointSubscriber>().ImplementedBy<DataPointSubscriber>()
                    .DependsOn(new { server = "localhost", port = 17892 }).OnCreate(d => d.Start()),

                Classes.FromThisAssembly().BasedOn<Controller>().LifestyleTransient());

            httpConfiguration.DependencyResolver = new WindsorWebApiDependencyResolver(container);
        }
    }
}