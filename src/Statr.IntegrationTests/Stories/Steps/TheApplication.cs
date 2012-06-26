using Statr.Storage;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheApplication
    {
        public static void IsStarted(StatrContext context)
        {
            context.Application = new IntegrationApplication();
            context.Application.Initialize();
            context.Container = context.Application.Container;

            var storageEngineFactory = context.Container.Resolve<IStorageEngineFactory>();

            context.StorageEngine = storageEngineFactory.Create(@"c:\dev\tmp\integration-tests");
        }
    }
}