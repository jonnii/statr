using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Statr.Configuration;
using Statr.Installers;

namespace Statr
{
    public abstract class StatrApplication : IDisposable
    {
        public IWindsorContainer Container { get; private set; }

        public void Initialize()
        {
            Container = new WindsorContainer("Configuration/Windsor.xml");

            var defaultInstallers = new IWindsorInstaller[]
            {
                new InfrastructureInstaller(LogFileName),
                new ConfigInstaller(), 
            };

            var installers = defaultInstallers.Concat(GetInstallers()).ToArray();

            Container.Install(installers);
        }

        public string LogFileName { get; set; }

        protected abstract IEnumerable<IWindsorInstaller> GetInstallers();

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}