using System;
using System.IO;
using FluentValidation;
using Statr.Infrastructure;
using YamlDotNet.RepresentationModel.Serialization;

namespace Statr.Configuration
{
    public class YamlConfigRepository : IConfigRepository
    {
        private const string ConfigurationFileName = "statr.yaml";

        private readonly IFileSystem fileSystem;

        private readonly IValidator<Config> configValidator;

        private readonly YamlSerializer<Config> serializer;

        public YamlConfigRepository(
            IFileSystem fileSystem,
            IValidator<Config> configValidator)
        {
            this.fileSystem = fileSystem;
            this.configValidator = configValidator;

            Path = Environment.CurrentDirectory;

            serializer = new YamlSerializer<Config>();
        }

        public string Path { get; set; }

        public void WriteConfiguration(Config config)
        {
            configValidator.ValidateAndThrow(config);

            fileSystem.WriteText(ConfigurationFileName, Serialize(config));
        }

        public Config GetConfiguration()
        {
            using (var reader = fileSystem.OpenText(ConfigurationFileName))
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