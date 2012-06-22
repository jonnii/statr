using Machine.Specifications;
using Statr.Config;

namespace Statr.Specifications.Config
{
    public class StorageConfigurationSpecification
    {
        [Subject(typeof(StorageConfiguration))]
        public class when_converting_to_xml
        {
            Establish context = () =>
                storageConfiguration = new StorageConfiguration();

            Because of = () =>
                xml = storageConfiguration.ToXml();

            It should_convert = () =>
                xml.ShouldNotBeEmpty();

            static StorageConfiguration storageConfiguration;

            static string xml;
        }

        [Subject(typeof(StorageConfiguration))]
        public class when_reading_from_xml_string
        {
            Establish context = () =>
            {
                original = new StorageConfiguration { Entries = new[] { new StorageEntry() } };
                xml = original.ToXml();
            };

            Because of = () =>
                read = StorageConfiguration.ReadXmlString(xml);

            It should_convert = () =>
                read.Entries.Length.ShouldEqual(original.Entries.Length);

            static StorageConfiguration original;

            static StorageConfiguration read;

            static string xml;
        }
    }
}
