using System.Collections.Generic;
using System.Linq;

namespace Statr.Configuration
{
    public class StorageEntry
    {
        public StorageEntry() { }

        public StorageEntry(string name, string pattern, params string[] retentions)
        {
            Name = name;
            Pattern = pattern;
            Retention = retentions.ToList();
        }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public List<string> Retention { get; set; }
    }
}