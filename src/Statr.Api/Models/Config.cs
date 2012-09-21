using System.Collections.Generic;

namespace Statr.Api.Models
{
    public class Config
    {
        public Config()
        {
            Entries = new List<Entry>();
        }

        public string Directory { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
