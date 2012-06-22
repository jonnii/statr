using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Routing;

namespace Statr.Installers
{
    public class RoutingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMetricRouter>().ImplementedBy<MetricRouter>());
        }
    }
}
