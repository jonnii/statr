using System;
using System.Collections.Generic;
using System.IO;

namespace Statr.Storage
{
    public class StorageTree : IStorageTree
    {
        private readonly IStorageEngine storageEngine;

        public StorageTree(
            IStorageEngine storageEngine,
            string name,
            IStorageTreeConfiguration treeConfiguration)
        {
            this.storageEngine = storageEngine;

            Name = name;
            FilePath = Path.Combine(storageEngine.RootFilePath, Name);
        }

        public string Name { get; set; }

        public string FilePath { get; private set; }

        public IStorageNode GetOrCreateNode(string name)
        {
            return GetOrCreateNode(name, c => { });
        }

        public IStorageNode GetOrCreateNode(string name, Action<IStorageNodeConfiguration> configuration)
        {
            var nodeConfiguration = StorageNodeConfiguration.Default;
            configuration(nodeConfiguration);

            var storageTree = new StorageNode(this, name, nodeConfiguration);

            storageTree.Initialize();

            return storageTree;
        }

        public IStorageNode GetNode(string name)
        {
            return null;
        }

        public void Store(string node, IEnumerable<DataPoint> dataPoints)
        {

        }

        public void Initialize()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }
    }
}