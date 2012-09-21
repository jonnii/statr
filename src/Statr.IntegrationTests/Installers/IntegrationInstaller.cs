using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Client;
using Statr.IntegrationTests.Support;
using Statr.Interactive;

namespace Statr.IntegrationTests.Installers
{
    public class IntegrationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<DirectClientTransport>(),
                Component.For<IMetricsGenerator>().ImplementedBy<MetricsGenerator>(),
                Component.For<IStatrClient>().UsingFactoryMethod(BuildClient));
        }

        private IStatrClient BuildClient(IKernel kernel)
        {
            var transport = kernel.Resolve<DirectClientTransport>();

            return new StatrClient(transport);
        }
    }
}
