using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Statr.Installers;

namespace Statr
{
    public abstract class StatrApplication : IDisposable
    {
        private WindsorContainer container;

        public IWindsorContainer Initialize()
        {
            container = new WindsorContainer("Config/Windsor.xml");

            var defaultInstallers = new IWindsorInstaller[]
            {
                new InfrastructureInstaller()
            };

            var installers = defaultInstallers.Concat(GetInstallers()).ToArray();

            container.Install(installers);

            return container;
        }

        protected abstract IEnumerable<IWindsorInstaller> GetInstallers();

        public void Dispose()
        {
            container.Dispose();
        }
    }
}