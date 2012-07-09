using Castle.Windsor;
using Statr.Configuration;
using Statr.Storage;

namespace Statr.IntegrationTests.Stories
{
    public class StatrContext : ScenarioContext
    {
        public IntegrationApplication Application { get; set; }

        public IWindsorContainer Container { get; set; }

        public IStorageEngine StorageEngine { get; set; }

        public Config Config { get; set; }

        public override void Dispose()
        {
            base.Dispose();

            Application.Dispose();
        }
    }
}