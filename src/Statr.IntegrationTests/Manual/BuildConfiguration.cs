using System;
using NUnit.Framework;
using Statr.Config;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class BuildConfiguration
    {
        [Test]
        public void Should()
        {
            var config = new StorageConfiguration
            {
                Entries = new[]
                {
                    new StorageEntry
                    {
                        Pattern = "^stats",
                        Retentions = new[]
                        {
                            new Retention
                            {
                                Frequency = "5s",
                                History = "10d"
                            }
                        }
                    }
                }
            };

            var xml = config.ToXml();

            Console.WriteLine(xml);
        }
    }
}
