using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SpeakEasy;

namespace Statr.Web.Windsor
{
    public class WebInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IHttpClient>().Instance(CreateHttpClient()),

                Classes.FromThisAssembly().BasedOn<Controller>().LifestyleTransient());
        }

        private IHttpClient CreateHttpClient()
        {
            return HttpClient.Create("http://localhost:17891");
        }
    }
}