using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Installers;
using Statr.IntegrationTests.Installers;

namespace Statr.IntegrationTests
{
    public class IntegrationApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new StorageInstaller();
            yield return new RoutingInstaller();
            yield return new IntegrationInstaller();
        }
    }
}
