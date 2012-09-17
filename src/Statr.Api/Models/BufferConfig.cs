using System.Collections.Generic;

namespace Statr.Api.Models
{
    public class BufferConfig
    {
        public string Type { get; set; }

        public Dictionary<string, object> Properties { get; set; }
    }
}