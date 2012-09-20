using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SignalR;

namespace Statr.Web.Windsor
{
    public class SignalResolver : DefaultDependencyResolver
    {
        private readonly IWindsorContainer container;

        // a form of laxy initialization is actually needed because the DefaultDependencyResolver starts initializing itself immediately
        // while we now want to store everything inside CastleWindsor, so the actual registration step have to be postponed until the 
        // container is available
        private readonly List<ComponentRegistration<object>> lazyRegistrations
            = new List<ComponentRegistration<object>>();

        public SignalResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;

            // perform the lazy registrations
            foreach (var c in lazyRegistrations)
                this.container.Register(c);

            lazyRegistrations.Clear();
        }

        public override object GetService(Type serviceType)
        {
            if (container.Kernel.HasComponent(serviceType))
                return container.Resolve(serviceType);
            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> objects;
            if (container.Kernel.HasComponent(serviceType))
                objects = container.ResolveAll(serviceType).Cast<object>();
            else
                objects = new object[] { };

            var originalContainerServices = base.GetServices(serviceType);
            if (originalContainerServices != null)
                return objects.Concat(originalContainerServices);

            return objects;
        }

        public override void Register(Type serviceType, Func<object> activator)
        {
            if (container != null)
                // cannot unregister components in windsor, so we use a trick
                container.Register(Component.For(serviceType).UsingFactoryMethod<object>(activator, true).OverridesExistingRegistration());
            else
                // lazy registration for when the container is up
                lazyRegistrations.Add(Component.For(serviceType).UsingFactoryMethod<object>(activator));

            // register the factory method in the default container too
            //base.Register(serviceType, activator);
        }
    }

    public static class WindsorTrickyExtensions
    {
        /// <summary>
        /// Overrideses the existing registration:
        /// to overide an existiong component registration you need to do two things:
        /// 1- give it a name.
        /// 2- set it as default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="componentRegistration">The component registration.</param>
        /// <returns></returns>
        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration) where T : class
        {
            return componentRegistration
                .Named(Guid.NewGuid().ToString())
                .IsDefault();
        }
    }
}