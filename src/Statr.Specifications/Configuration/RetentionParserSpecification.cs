using System;
using Machine.Specifications;
using Statr.Configuration;

namespace Statr.Specifications.Configuration
{
    public class RetentionParserSpecification
    {
        [Subject(typeof(RetentionParser))]
        public class when_parsing
        {
            Because of = () =>
                retention = RetentionParser.Parse("2m:10w");

            It should_parse_days = () =>
                retention.Frequency.ShouldEqual(120);

            It should_parse_history = () =>
                retention.History.ShouldEqual(6048000);

            static Retention retention;
        }

        [Subject(typeof(RetentionParser))]
        public class when_parsing_gibberish
        {
            Because of = () =>
                exception = Catch.Exception(() => RetentionParser.Parse("scribble/scrabble"));

            It should_throw = () =>
                exception.ShouldNotBeNull();

            static Exception exception;
        }

        [Subject(typeof(RetentionParser))]
        public class when_parsing_with_too_many_bits
        {
            Because of = () =>
                exception = Catch.Exception(() => RetentionParser.Parse("fribble:frabble:frobble"));

            It should_throw = () =>
                exception.ShouldNotBeNull();

            static Exception exception;
        }

        [Subject(typeof(RetentionParser))]
        public class when_parsing_with_unknown_date_format
        {
            Because of = () =>
                exception = Catch.Exception(() => RetentionParser.Parse("1w:30k"));

            It should_throw = () =>
                exception.ShouldNotBeNull();

            static Exception exception;
        }
    }
}
