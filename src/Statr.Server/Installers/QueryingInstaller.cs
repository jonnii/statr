using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statr.Server.Querying;

namespace Statr.Server.Installers
{
    public class QueryingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryEngine>().ImplementedBy<QueryEngine>());
        }
    }
}
