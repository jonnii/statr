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
            Entries = new List<Entry>();
        }

        public List<Entry> Entries { get; set; }

        public IEnumerable<Retention> GetRetentions(string metricName)
        {
            var entry = GetEntryMaching(metricName);

            if (entry == null)
            {
                var message = string.Format(
                    "Could not find a config storage entry to match the metric name: {0}", metricName);

                throw new ConfigException(message);
            }

            return entry.Retentions.Select(RetentionParser.Parse);
        }

        private Entry GetEntryMaching(string name)
        {
            return Entries.FirstOrDefault(e => e.Matches(name));
        }

        public Entry AddEntry(string name, string pattern, params string[] retentions)
        {
            var entry = new Entry(name, pattern, retentions);
            Entries.Add(entry);
            return entry;
        }

        public BufferConfig GetStorage(string name)
        {
            var entry = GetEntryMaching(name);

            return entry == null
                ? BufferConfig.Default
                : entry.Buffer ?? BufferConfig.Default;
        }
    }
}