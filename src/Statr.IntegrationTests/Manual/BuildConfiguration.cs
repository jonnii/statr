using System;
using System.Collections.Generic;
using NUnit.Framework;
using Statr.Configuration;
using Statr.Server.Configuration;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class BuildConfiguration : ContainerTest
    {
        [Test, Explicit]
        public void Should()
        {
            var config = new Config
            {
                Entries = new List<Entry>
                {
                    new Entry
                    {
                        Pattern = "^stats",
                        Retentions = new List<string>
                        {
                            "every 5s for 10d"
                        }
                    }
                }
            };

            var configurationService = GetContainer().Resolve<IConfigRepository>();

            var serialized = configurationService.Serialize(config);

            Console.WriteLine(serialized);
        }
    }
}
