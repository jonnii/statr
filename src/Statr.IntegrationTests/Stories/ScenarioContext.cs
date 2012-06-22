using System;
using System.Collections.Generic;
using System.Linq;

namespace Statr.IntegrationTests.Stories
{
    public class ScenarioContext : IDisposable
    {
        private readonly Dictionary<string, object> contextItemsById = new Dictionary<string, object>();

        private readonly Dictionary<Type, List<object>> contextItemsByType = new Dictionary<Type, List<object>>();

        public T Get<T>()
        {
            if (!contextItemsByType.ContainsKey(typeof(T)))
            {
                throw new ArgumentException(
                    string.Format("Cannot find any item in the scenario context of type {0}", typeof(T).Name));
            }

            var items = contextItemsByType[typeof(T)];
            if (items.Count > 1)
            {
                throw new ArgumentException(
                    string.Format(
                        "There are more than one item in the scenario context of type {0}.  Maybe add and retrieve using a key",
                        typeof(T).Name));
            }

            return (T)items.Single();
        }

        public T Get<T>(string id)
        {
            if (!contextItemsById.ContainsKey(id))
            {
                throw new ArgumentException(
                    string.Format("Cannot find any item in the scenario context with the id {0}", id), "id");
            }

            return (T)contextItemsById[id];
        }

        public void Replace<T>(T entity)
        {
            if (contextItemsByType.ContainsKey(typeof(T)))
            {
                contextItemsByType[typeof(T)].Clear();
            }

            contextItemsByType[typeof(T)].Add(entity);
        }

        public void Add<T>(T entity)
        {
            if (!contextItemsByType.ContainsKey(typeof(T)))
            {
                contextItemsByType.Add(typeof(T), new List<object>());
            }

            contextItemsByType[typeof(T)].Add(entity);
        }

        public void Add<T>(string id, T entity)
        {
            if (contextItemsById.ContainsKey(id))
            {
                throw new ArgumentException(
                    string.Format("Already have the item {0} in the scenario context", id), "id");
            }

            Add(entity);
            contextItemsById.Add(id, entity);
        }

        public virtual void Dispose()
        {
            foreach (var disposableItem in contextItemsById.Values.OfType<IDisposable>())
            {
                disposableItem.Dispose();
            }

            foreach (var disposableItem in contextItemsByType.SelectMany(t => t.Value).OfType<IDisposable>())
            {
                disposableItem.Dispose();
            }

            contextItemsById.Clear();
            contextItemsByType.Clear();
        }
    }
}