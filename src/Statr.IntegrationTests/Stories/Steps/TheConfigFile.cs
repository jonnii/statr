using NUnit.Framework;
using Statr.Configuration;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheConfigFile
    {
        public static void IsRead(StatrContext context)
        {
            var configurationService = context.Container.Resolve<IConfigService>();
            context.Config = configurationService.GetStorageConfiguration();
        }

        public static void ShouldHaveBeenRead(StatrContext context)
        {
            Assert.That(context.Config, Is.Not.Null);
        }
    }
}