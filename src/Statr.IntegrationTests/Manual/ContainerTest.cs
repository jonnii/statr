using Castle.Windsor;

namespace Statr.IntegrationTests.Manual
{
    public class ContainerTest
    {
        protected IWindsorContainer GetContainer()
        {
            var application = new IntegrationApplication();
            application.Initialize();

            return application.Container;
        }
    }
}