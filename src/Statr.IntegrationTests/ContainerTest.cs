using System;
using Castle.Windsor;

namespace Statr.IntegrationTests
{
    public class ContainerTest
    {
        protected IWindsorContainer GetContainer(params Action<StatrApplication>[] builders)
        {
            var application = new IntegrationApplication();

            foreach (var builder in builders)
            {
                builder(application);
            }

            application.Initialize();

            return application.Container;
        }
    }
}