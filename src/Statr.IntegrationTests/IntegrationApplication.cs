using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Installers;

namespace Statr.IntegrationTests
{
    public class IntegrationApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new StorageInstaller();
        }
    }
}
