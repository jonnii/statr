using Castle.Core.Logging;
using ServiceStack.ServiceInterface;
using Statr.Configuration;

namespace Statr.Management.Config
{
    public class ConfigService : RestServiceBase<ConfigRequest>
    {
        private readonly IConfigRepository configRepository;

        public ConfigService(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public override object OnGet(ConfigRequest request)
        {
            Logger.Info("Getting configuration");

            return configRepository.GetConfiguration();
        }
    }
}
