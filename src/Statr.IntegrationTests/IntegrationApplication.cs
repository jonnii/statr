using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.IntegrationTests.Installers;
using Statr.Server.Installers;

namespace Statr.IntegrationTests
{
    public class IntegrationApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new ConfigInstaller();
            yield return new RoutingInstaller();
            yield return new StorageInstaller();
            yield return new IntegrationInstaller();
            yield return new QueryingInstaller();
            yield return new PublishingInstaller();
        }
    }
}
