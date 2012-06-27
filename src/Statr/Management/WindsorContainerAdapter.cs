using Castle.Windsor;
using ServiceStack.Configuration;

namespace Statr.Management
{
    public class WindsorContainerAdapter : IContainerAdapter
    {
        private readonly IWindsorContainer container;

        public WindsorContainerAdapter(IWindsorContainer container)
        {
            this.container = container;
        }

        public T TryResolve<T>()
        {
            return container.Kernel.HasComponent(typeof(T))
                       ? container.Resolve<T>()
                       : default(T);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}