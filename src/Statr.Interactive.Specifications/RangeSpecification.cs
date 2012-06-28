using System;
using Machine.Specifications;

namespace Statr.Interactive.Specifications
{
    public class RangeSpecification
    {
        [Subject(typeof(Range))]
        public class when_parsing
        {
            Because of = () =>
                range = Range.Parse("100-200");

            It should_parse_form = () =>
                range.From.ShouldEqual(100);

            It should_parse_to = () =>
                range.To.ShouldEqual(200);

            static Range range;
        }

        [Subject(typeof(Range))]
        public class when_parsing_when_from_is_lower_than_to
        {
            Because of = () =>
                 exception = Catch.Exception(() => Range.Parse("200-100"));

            It should_throw_format_exception = () =>
                exception.ShouldBeOfType<FormatException>();

            static Exception exception;
        }
    }
}