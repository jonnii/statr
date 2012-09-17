using FluentValidation.Results;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Configuration;
using Statr.Server.Specifications.Fixtures;

namespace Statr.Server.Specifications.Configuration
{
    public class ConfigValidatorSpecification
    {
        [Subject(typeof(ConfigValidator))]
        public class when_valid : WithSubject<ConfigValidator>
        {
            Because of = () =>
                result = Subject.Validate(ConfigFixture.Create());

            It should_be_valid = () =>
                result.IsValid.ShouldBeTrue();

            static ValidationResult result;
        }

        [Subject(typeof(ConfigValidator))]
        public class with_invalid_entry : WithSubject<ConfigValidator>
        {
            Because of = () =>
                result = Subject.Validate(ConfigFixture.CreateWithInvalidEntry());

            It should_be_valid = () =>
                result.IsValid.ShouldBeFalse();

            static ValidationResult result;
        }
    }
}
