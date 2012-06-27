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
                Component.For<ManagementAppHost>(),
                Component.For<ManagementBootstrapper>().DependsOn(new { container }).StartUsingMethod("Start"));
        }
    }
}
