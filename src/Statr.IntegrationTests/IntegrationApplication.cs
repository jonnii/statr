﻿using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Configuration;
using Statr.Installers;
using Statr.IntegrationTests.Installers;
using Statr.Server.Configuration;
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
        }
    }
}
