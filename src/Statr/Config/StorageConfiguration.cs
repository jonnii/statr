using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Statr.Config
{
    public class StorageConfiguration
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(StorageConfiguration));

        public StorageEntry[] Entries { get; set; }

        public static StorageConfiguration ReadXmlString(string xml)
        {
            using (var stringReader = new StringReader(xml))
            {
                return (StorageConfiguration)serializer.Deserialize(stringReader);
            }
        }

        public string ToXml()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    serializer.Serialize(writer, this);
                    writer.Flush();
                    return Encoding.Default.GetString(stream.GetBuffer());
                }
            }
        }
    }
}