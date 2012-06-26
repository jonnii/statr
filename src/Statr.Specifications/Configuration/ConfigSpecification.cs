using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Statr.Configuration;

namespace Statr.Specifications.Configuration
{
    public class ConfigSpecification
    {
        [Subject(typeof(Config))]
        public class in_general
        {
            Establish context = () =>
                config = Config.Build();

            It should_have_empty_entries = () =>
                config.Entries.ShouldBeEmpty();

            static Config config;
        }

        [Subject(typeof(Config))]
        public class when_building
        {
            Because of = () =>
                config = Config.Build(c => c.AddEntry("default", "^stats.*"));

            It should_have_entry = () =>
                config.Entries.Count.ShouldEqual(1);

            static Config config;
        }

        [Subject(typeof(Config))]
        public class when_getting_route_definitions : with_config
        {
            Because of = () =>
                definitions = config.GetRouteDefinitions("stats.fribble.frabble");

            It should_create_definition_for_each_retention_level = () =>
                definitions.Count().ShouldEqual(2);

            static IEnumerable<Retention> definitions;
        }

        public class with_config
        {
            Establish context = () =>
            {
                config = Config.Build(
                    c => c.AddEntry("default", "^stats.", "2m:10d", "10m:30d"),
                    c => c.AddEntry("things", "^things."));
            };

            protected static Config config;
        }
    }
}
