using System;
using System.Collections.Generic;
using Castle.Core.Logging;

namespace Statr.Storage
{
    public class StorageEngine : IStorageEngine
    {
        public StorageEngine(string rootFilePath)
        {
            RootFilePath = rootFilePath;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public string RootFilePath { get; private set; }

        public IStorageTree CreateTree(string name)
        {
            return CreateTree(name, c => { });
        }

        public IStorageTree CreateTree(string name, Action<IStorageTreeConfiguration> configuration)
        {
            Logger.DebugFormat("Creating storage tree: {0}", name);

            var treeConfiguration = StorageTreeConfiguration.Default;
            configuration(treeConfiguration);

            var storageTree = new StorageTree(this, name, treeConfiguration);

            storageTree.Initialize();

            return storageTree;
        }
    }
}
