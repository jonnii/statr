using Castle.Windsor;
using Statr.Server.Configuration;
using Statr.Server.Querying;
using Statr.Server.Storage;

namespace Statr.IntegrationTests.Stories
{
    public class StatrContext : ScenarioContext
    {
        public IntegrationApplication Application { get; set; }

        public Config Config { get; set; }

        public IWindsorContainer Container { get; set; }

        public IStorageEngine StorageEngine
        {
            get { return Container.Resolve<IStorageEngine>(); }
        }

        public IQueryEngine QueryEngine
        {
            get { return Container.Resolve<IQueryEngine>(); }
        }

        public override void Dispose()
        {
            base.Dispose();

            Application.Dispose();
        }
    }
}