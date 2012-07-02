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
        }

        public override object OnGet(ConfigRequest request)
        {
            return configRepository.GetConfiguration();
        }
    }
}
