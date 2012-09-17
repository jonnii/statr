using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Server.Installers.Factories;
using Statr.Server.Storage;
using Statr.Server.Storage.Engine;

namespace Statr.Server.Installers
{
    public class StorageInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IBufferStrategy>().WithServiceSelf(),
                Component.For<IBufferStrategyFactory>().AsFactory(c => c.SelectedWith<StorageStrategySelector>()),
                Component.For<StorageStrategySelector, ITypedFactoryComponentSelector>(),

                Component.For<IStorageEngine>().ImplementedBy<StorageEngine>(),
                Component.For<IBucketRepository>().ImplementedBy<BucketRepository>(),
                Component.For<IDataPointRepository>().ImplementedBy<DataPointRepository>(),
                Component.For<IDataPointCache>().ImplementedBy<DataPointCache>().StartUsingMethod("Start"),
                Component.For<DataPointBuffer>().StartUsingMethod("Start"));
        }
    }
}
