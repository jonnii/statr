using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;

namespace Statr.IntegrationTests.Stories.Configuration
{
    [TestFixture]
    public class ReadConfigurationFeature : StatrStory
    {
        [Test]
        public void ShouldReadConfigurationFiles()
        {
            Given(TheApplication.IsStarted).
            When(TheConfigFile.IsRead).
            Then(TheConfigFile.ShouldHaveBeenRead);
        }
    }
}
