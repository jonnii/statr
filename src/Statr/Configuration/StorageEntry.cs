using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Statr.Configuration
{
    public class StorageEntry
    {
        public StorageEntry() { }

        public StorageEntry(string name, string pattern, params string[] retentions)
        {
            Name = name;
            Pattern = pattern;
            Retentions = retentions.ToList();
        }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public List<string> Retentions { get; set; }

        public bool Matches(string metricName)
        {
            return Regex.IsMatch(metricName, Pattern);
        }
    }
}