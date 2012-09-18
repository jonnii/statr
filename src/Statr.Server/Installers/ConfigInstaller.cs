using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Statr.Server.Configuration;

namespace Statr.Server.Installers
{
    public class ConfigInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn(typeof(IValidator<>)).WithService.FromInterface(typeof(IValidator<>)),

                Component.For<IConfigService>().ImplementedBy<ConfigService>().StartUsingMethod("Start"),
                Component.For<IConfigRepository>().ImplementedBy<YamlConfigRepository>());
        }
    }
}
