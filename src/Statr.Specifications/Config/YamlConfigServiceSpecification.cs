using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;

namespace Statr.Specifications.Config
{
    public class YamlConfigServiceSpecification
    {
        [Subject(typeof(YamlConfigService))]
        public class when_serializing_configuration : with_configuration_file
        {
            Because of = () =>
                serialized = Subject.Serialize(config);

            It should_serialize = () =>
                serialized.ShouldNotBeEmpty();

            static string serialized;
        }

        public class with_configuration_file : WithSubject<YamlConfigService>
        {
            Establish context = () =>
                config = new Configuration.Config();

            protected static Configuration.Config config;
        }
    }
}
