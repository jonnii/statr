using FluentValidation;
using FluentValidation.Results;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Infrastructure;
using Statr.Server.Configuration;
using Statr.Server.Specifications.Fixtures;

namespace Statr.Server.Specifications.Configuration
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

        [Subject(typeof(YamlConfigRepository))]
        public class when_writing_config : with_configuration_file
        {
            Establish context = () =>
                The<IValidator<Config>>().WhenToldTo(v => v.Validate(Param.IsAny<Config>()))
                    .Return(new ValidationResult());

            Because of = () =>
                Subject.WriteConfiguration(ConfigFixture.CreateWithInvalidEntry());

            It should_write_config = () =>
                The<IFileSystem>().WasToldTo(s => s.WriteText(Param.IsAny<string>(), Param.IsAny<string>()));
        }

        public class with_configuration_file : WithSubject<YamlConfigRepository>
        {
            Establish context = () =>
                config = new Config();

            protected static Config config;
        }
    }
}
