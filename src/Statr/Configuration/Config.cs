using System;
using System.Collections.Generic;
using System.Linq;

namespace Statr.Configuration
{
    public class Config
    {
        public static Config Build(params Action<Config>[] builders)
        {
            var config = new Config();

            foreach (var builder in builders)
            {
                builder(config);
            }

            return config;
        }

        public Config()
        {
            Entries = new List<StorageEntry>();
        }

        public List<StorageEntry> Entries { get; set; }

        public IEnumerable<Retention> GetRouteDefinitions(string metricName)
        {
            var entry = Entries.FirstOrDefault(e => e.Matches(metricName));

            if (entry == null)
            {
                var message = string.Format(
                    "Could not find a config storage entry to match the metric name: {0}", metricName);

                throw new ConfigException(message);
            }

            return entry.Retentions.Select(RetentionParser.Parse);
        }

        public StorageEntry AddEntry(string name, string pattern, params string[] retentions)
        {
            var entry = new StorageEntry(name, pattern, retentions);
            Entries.Add(entry);
            return entry;
        }
    }
}