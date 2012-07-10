using System.Collections.Generic;

namespace Statr.Configuration
{
    public class StorageConfig
    {
        public static StorageConfig Default
        {
            get
            {
                return new StorageConfig
                {
                    Type = "BufferedStorageStrategy"
                };
            }
        }

        public string Type { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public override string ToString()
        {
            return string.Concat("[StorageConfig Type=", Type, "]");
        }
    }
}