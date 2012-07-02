using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var root = WhereAmI(GetType().Assembly);
            var configPath = Path.Combine(root, "Configuration/Windsor.xml");

            Container = new WindsorContainer(configPath);

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

        private string WhereAmI(Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}