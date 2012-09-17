using System.Collections.Generic;

namespace Statr.Api.Models
{
    public class Entry
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public BufferConfig Buffer
        {
            get;
            set;
        }

        public List<string> Retentions { get; set; }
    }
}