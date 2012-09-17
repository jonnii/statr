using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Statr.Server.Configuration
{
    public class Entry
    {
        public Entry() { }

        public Entry(string name, string pattern, params string[] retentions)
        {
            Name = name;
            Pattern = pattern;
            Retentions = retentions.ToList();
        }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public BufferConfig Buffer
        {
            get;
            set;
        }

        public List<string> Retentions { get; set; }

        public bool Matches(string metricName)
        {
            return Regex.IsMatch(metricName, Pattern);
        }
    }
}