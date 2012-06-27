﻿using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Installers;
using Statr.Management;

namespace Statr
{
    public class StatrServerApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new StorageInstaller();
            yield return new RoutingInstaller();
            yield return new ServerInstaller();
            yield return new ManagementInstaller();
        }
    }
}
