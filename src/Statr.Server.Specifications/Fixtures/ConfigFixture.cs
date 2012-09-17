using System;
using Statr.Server.Configuration;

namespace Statr.Server.Specifications.Fixtures
{
    public static class ConfigFixture
    {
        public static Config Create(params Action<Config>[] builders)
        {
            var config = new Config();
            config.AddEntry("default", ".+", "30d:1y");

            foreach (var builder in builders)
            {
                builder(config);
            }

            return config;
        }

        public static Config CreateWithInvalidEntry(params Action<Config>[] builders)
        {
            return Create(c => c.AddEntry(null, "pattern", "30m:1d"));
        }
    }
}
