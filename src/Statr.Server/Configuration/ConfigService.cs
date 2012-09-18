using Castle.Core.Logging;

namespace Statr.Server.Configuration
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepository configRepository;

        private readonly IConfigWatcher[] configWatchers;

        public ConfigService(IConfigRepository configRepository, IConfigWatcher[] configWatchers)
        {
            this.configRepository = configRepository;
            this.configWatchers = configWatchers;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            Logger.Debug("Starting config service");
            var config = configRepository.GetConfiguration();

            foreach (var watcher in configWatchers)
            {
                Logger.DebugFormat("Notifying config changed: {0}", watcher);

                watcher.NotifyConfigChanged(config);
            }
        }
    }
}