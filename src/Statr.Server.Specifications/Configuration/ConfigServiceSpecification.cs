using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Configuration;

namespace Statr.Server.Specifications.Configuration
{
    public class ConfigServiceSpecification
    {
        [Subject(typeof(ConfigService))]
        public class When_Context : with_config_service
        {
            Establish context = () => { };

            Because of = () =>
                Subject.Start();

            It should_load_configuration = () =>
                configRepository.WasToldTo(r => r.GetConfiguration());

            It should_notify_all_watchers = () =>
                configWatcher.WasToldTo(c => c.NotifyConfigChanged(Param.IsAny<Config>())).Twice();
        }

        public class with_config_service : WithFakes
        {
            Establish context = () =>
            {
                configRepository = An<IConfigRepository>();
                configWatcher = An<IConfigWatcher>();

                Subject = new ConfigService(configRepository, new[] { configWatcher, configWatcher });
            };

            protected static ConfigService Subject;

            protected static IConfigRepository configRepository;

            protected static IConfigWatcher configWatcher;
        }
    }
}
