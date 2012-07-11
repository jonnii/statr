using System.Collections.Generic;
using Statr.Storage.Strategies;

namespace Statr.Configuration
{
    public class BufferConfig
    {
        public static BufferConfig Default
        {
            get
            {
                return new BufferConfig
                {
                    Type = typeof(BufferedStrategy).Name
                };
            }
        }

        public string Type { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public override string ToString()
        {
            return string.Concat("[BufferConfig Type=", Type, "]");
        }
    }
}