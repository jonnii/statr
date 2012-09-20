using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Server.Publishing;

namespace Statr.Server.Installers
{
    public class PublishingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPublisher>().ImplementedBy<Publisher>().StartUsingMethod("Start"));
        }
    }
}
