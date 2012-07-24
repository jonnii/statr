using System;
using System.Web.Http.SelfHost;
using Castle.Core.Logging;

namespace Statr.Management
{
    public class ManagementBootstrapper : IDisposable
    {
        private readonly HttpSelfHostConfiguration httpSelfHostConfiguration;

        private HttpSelfHostServer selfHostServer;

        public ManagementBootstrapper(
            HttpSelfHostConfiguration httpSelfHostConfiguration)
        {
            this.httpSelfHostConfiguration = httpSelfHostConfiguration;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            selfHostServer = new HttpSelfHostServer(httpSelfHostConfiguration);
            selfHostServer.OpenAsync();

            Logger.Info("Started management app host");
        }

        public void Dispose()
        {
            Logger.Info("Stopping management app host");
            selfHostServer.CloseAsync().Wait();
        }
    }
}