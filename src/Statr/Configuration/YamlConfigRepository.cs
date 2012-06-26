using System;
using System.IO;
using YamlDotNet.RepresentationModel.Serialization;

namespace Statr.Configuration
{
    public class YamlConfigRepository : IConfigRepository
    {
        private const string ConfigurationFileName = "statr.yaml";

        private readonly YamlSerializer<Config> serializer;

        public YamlConfigRepository()
        {
            Path = Environment.CurrentDirectory;

            serializer = new YamlSerializer<Config>();
        }

        public string Path { get; set; }

        public void WriteConfiguration(Config config)
        {

        }

        public Config GetConfiguration()
        {
            using (var reader = File.OpenText(ConfigurationFileName))
            {
                return serializer.Deserialize(reader);
            }
        }

        public string Serialize(Config config)
        {
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, config);
                return writer.ToString();
            }
        }
    }
}