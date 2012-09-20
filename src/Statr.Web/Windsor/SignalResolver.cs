using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using SignalR;

namespace Statr.Web.Windsor
{
    public class SignalResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public SignalResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType).Cast<object>();
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            throw new NotImplementedException();
        }
    }
}