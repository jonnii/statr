using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;

namespace Statr.Specifications.Configuration
{
    public class YamlConfigRepositorySpecification
    {
        [Subject(typeof(YamlConfigRepository))]
        public class when_serializing_configuration : with_configuration_file
        {
            Because of = () =>
                serialized = Subject.Serialize(config);

            It should_serialize = () =>
                serialized.ShouldNotBeEmpty();

            static string serialized;
        }

        public class with_configuration_file : WithSubject<YamlConfigRepository>
        {
            Establish context = () =>
                config = new Config();

            protected static Config config;
        }
    }
}
