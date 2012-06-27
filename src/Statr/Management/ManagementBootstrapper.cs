using System;
using Castle.Core.Logging;
using Castle.Windsor;

namespace Statr.Management
{
    public class ManagementBootstrapper
    {
        private readonly ManagementAppHost managementAppHost;

        private readonly IWindsorContainer container;

        public ManagementBootstrapper(
            ManagementAppHost managementAppHost,
            IWindsorContainer container)
        {
            this.managementAppHost = managementAppHost;
            this.container = container;
        }

        public ILogger Logger { get; set; }

        public int Port { get; set; }

        public void Start()
        {
            var listeningOn = string.Format("http://127.0.0.1:{0}/", Port);

            Logger.InfoFormat("Starting management app host on: {0}", listeningOn);

            managementAppHost.Init();
            managementAppHost.Container.Adapter = new WindsorContainerAdapter(container);
            managementAppHost.Start(listeningOn);

            Logger.Info("Started management app host");
        }
    }
}