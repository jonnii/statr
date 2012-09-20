using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Server.Installers;
using Statr.Server.Management;

namespace Statr.Server
{
    public class StatrServerApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new ConfigInstaller();
            yield return new StorageInstaller();
            yield return new RoutingInstaller();
            yield return new ServerInstaller();
            yield return new ManagementInstaller();
            yield return new QueryingInstaller();
            yield return new PublishingInstaller();
        }
    }
}
