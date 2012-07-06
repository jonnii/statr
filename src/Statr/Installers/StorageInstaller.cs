using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Routing;
using Statr.Storage;

namespace Statr.Installers
{
    public class StorageInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IStorageEngineFactory>().AsFactory(),
                Component.For<IStorageEngine>().ImplementedBy<StorageEngine>(),

                Component.For<IBucketRepository>().ImplementedBy<BucketRepository>(),
                Component.For<IDataPointRepository>().ImplementedBy<DataPointRepository>(),
                Component.For<IDataPointCache>().ImplementedBy<DataPointCache>().Forward<IDataPointSubscriber>());
        }
    }
}
