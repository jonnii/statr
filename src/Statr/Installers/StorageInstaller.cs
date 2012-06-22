using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Storage;

namespace Statr.Installers
{
    public class StorageInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IStorageEngineFactory>().AsFactory(),
                Component.For<IStorageEngine>().ImplementedBy<StorageEngine>());
        }
    }
}
