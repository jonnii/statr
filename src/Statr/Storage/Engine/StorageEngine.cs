using System;
using Castle.Core.Logging;

namespace Statr.Storage.Engine
{
    public class StorageEngine : IStorageEngine
    {
        public StorageEngine()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public string RootFilePath { get; set; }

        public IStorageTree GetOrCreateTree(string name)
        {
            return GetOrCreateTree(name, c => { });
        }

        public IStorageTree GetOrCreateTree(string name, Action<IStorageTreeConfiguration> configuration)
        {
            Logger.DebugFormat("Creating storage tree: {0}", name);

            var treeConfiguration = StorageTreeConfiguration.Default;
            configuration(treeConfiguration);

            var storageTree = new StorageTree(this, name, treeConfiguration);

            storageTree.Initialize();

            return storageTree;
        }

        public IDataPointWriter GetWriter(BucketReference bucketReference)
        {
            return new StorageEngineDataPointWriter(this, bucketReference);
        }
    }
}
