using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;

namespace Statr.IntegrationTests.Stories.Storage
{
    [TestFixture]
    public class CreatingStorageFeature : StatrStory
    {
        [Test]
        public void CreatingTree()
        {
            Given(TheStorageEngine.IsStarted).
            When(TheStorageEngine.Executes(s => s.CreateTree("storage-tree"))).
            Then(TheStorageEngine.ShouldHaveCreatedDirectory("storage-tree"));
        }
    }
}
