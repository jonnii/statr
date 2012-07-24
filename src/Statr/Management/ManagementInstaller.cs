using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;
using System.Web.Http.SelfHost;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Statr.Management
{
    public class ManagementInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly()
                    .BasedOn<IHttpController>().WithServiceSelf().LifestyleTransient(),

                Component.For<IHttpControllerSelector>().ImplementedBy<DefaultHttpControllerSelector>(),
                Component.For<IHttpControllerActivator>().ImplementedBy<DefaultHttpControllerActivator>().LifeStyle.Transient,
                Component.For<IHttpActionSelector>().ImplementedBy<ApiControllerActionSelector>().LifeStyle.Transient,
                Component.For<IActionValueBinder>().ImplementedBy<DefaultActionValueBinder>().LifeStyle.Transient,
                Component.For<IHttpActionInvoker>().ImplementedBy<ApiControllerActionInvoker>().LifeStyle.Transient,
                Component.For<HttpConfigurationFactory>().DependsOn(new { container }),

                // http configuration needs to be registered with the container otherwise we can't
                // create controllers
                Component.For<HttpConfiguration>()
                    .Forward<HttpSelfHostConfiguration>()
                    .UsingFactoryMethod(k => k.Resolve<HttpConfigurationFactory>().Create()),

                Component.For<ManagementBootstrapper>().StartUsingMethod("Start"));
        }
    }
}
