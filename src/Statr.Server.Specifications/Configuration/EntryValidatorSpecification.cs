using System;
using FluentValidation.Results;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Server.Configuration;

namespace Statr.Server.Specifications.Configuration
{
    public class EntryValidatorSpecification
    {
        [Subject(typeof(EntryValidator))]
        public class when_valid : WithSubject<EntryValidator>
        {
            Because of = () =>
                result = Subject.Validate(build_entry());

            It should_be_valid = () =>
                result.IsValid.ShouldBeTrue();

            static ValidationResult result;
        }

        [Subject(typeof(EntryValidator))]
        public class without_name : WithSubject<EntryValidator>
        {
            Because of = () =>
                result = Subject.Validate(build_entry(c => c.Name = null));

            It should_be_valid = () =>
                result.IsValid.ShouldBeFalse();

            static ValidationResult result;
        }

        [Subject(typeof(EntryValidator))]
        public class without_pattern : WithSubject<EntryValidator>
        {
            Because of = () =>
                result = Subject.Validate(build_entry(c => c.Pattern = null));

            It should_be_valid = () =>
                result.IsValid.ShouldBeFalse();

            static ValidationResult result;
        }

        private static Entry build_entry(params Action<Entry>[] builders)
        {
            var entry = new Entry("name", "pattern", "1m:20d");

            foreach (var builder in builders)
            {
                builder(entry);
            }

            return entry;
        }
    }
}