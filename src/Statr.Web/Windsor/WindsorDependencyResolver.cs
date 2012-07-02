using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Windsor;

namespace Statr.Web.Windsor
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType)
                       ? container.Resolve(serviceType)
                       : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (object[])container.ResolveAll(serviceType);
        }
    }
}