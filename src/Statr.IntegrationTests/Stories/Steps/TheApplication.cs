using Statr.Server.Storage;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheApplication
    {
        public static void IsStarted(StatrContext context)
        {
            context.Application = new IntegrationApplication();
            context.Application.Initialize();
            context.Container = context.Application.Container;

            context.StorageEngine = context.Container.Resolve<IStorageEngine>();
        }
    }
}