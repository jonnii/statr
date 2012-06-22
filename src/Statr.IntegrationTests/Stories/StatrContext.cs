using Castle.Windsor;
using Statr.Storage;

namespace Statr.IntegrationTests.Stories
{
    public class StatrContext : ScenarioContext
    {
        public IntegrationApplication Application { get; set; }

        public IWindsorContainer Container { get; set; }

        public IStorageEngine StorageEngine { get; set; }
    }
}