using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Statr.Configuration;

namespace Statr.Installers.Factories
{
    public class StorageStrategySelector : DefaultTypedFactoryComponentSelector
    {
        private readonly IConfigRepository configRepository;

        public StorageStrategySelector(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;
        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            var storage = GetStorageConfig(arguments);
            return Type.GetType("Statr.Storage.Strategies." + storage.Type, true);
        }

        protected override System.Collections.IDictionary GetArguments(MethodInfo method, object[] arguments)
        {
            var storage = GetStorageConfig(arguments);
            return storage.Properties;
        }

        private BufferConfig GetStorageConfig(IList<object> arguments)
        {
            var bucketReference = (BucketReference)arguments[0];
            var config = configRepository.GetConfiguration();

            return config.GetStorage(bucketReference.Name);
        }
    }
}