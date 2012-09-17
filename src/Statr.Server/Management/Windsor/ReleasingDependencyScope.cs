using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Statr.Server.Management.Windsor
{
    internal class ReleasingDependencyScope : IDependencyScope
    {
        private readonly IDependencyScope scope;

        private readonly Action<object> release;

        private readonly List<object> instances;

        public ReleasingDependencyScope(IDependencyScope scope, Action<object> release)
        {
            if (scope == null)
            {
                throw new ArgumentNullException("scope");
            }

            if (release == null)
            {
                throw new ArgumentNullException("release");
            }

            this.scope = scope;
            this.release = release;

            instances = new List<object>();
        }

        public object GetService(Type t)
        {
            var service = scope.GetService(t);

            AddToScope(service);

            return service;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var services = scope.GetServices(t);

            AddToScope(services);

            return services;
        }

        public void Dispose()
        {
            foreach (object instance in instances)
            {
                release(instance);
            }

            instances.Clear();
        }

        private void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                instances.AddRange(services);
            }
        }
    }
}