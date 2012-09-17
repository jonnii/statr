using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace Statr.Web.Windsor
{
    public class WindsorWebApiDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorWebApiDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public object GetService(Type t)
        {
            var component = container.Kernel.HasComponent(t)
                                ? container.Resolve(t)
                                : null;

            return component;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var resolved = container.ResolveAll(t).Cast<object>().ToArray();

            return resolved;
        }

        public IDependencyScope BeginScope()
        {
            return new ReleasingDependencyScope(this, container.Release);
        }

        public void Dispose()
        {
        }
    }
}