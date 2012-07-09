using Castle.Facilities.TypedFactory;
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
                Component.For<IMetricParser>().ImplementedBy<MetricParser>(),

                Component.For<IMetricRouter>().ImplementedBy<MetricRouter>(),
                Component.For<IMetricRouteManager>().ImplementedBy<MetricRouteManager>(),
                Component.For<IMetricRouteFactory>().AsFactory(),
                Component.For<IMetricRoute>().ImplementedBy<MetricRoute>().LifestyleTransient(),

                Component.For<IDataPointStream>().ImplementedBy<DataPointStream>());
        }
    }
}
