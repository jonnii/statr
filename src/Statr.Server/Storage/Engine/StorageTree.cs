using System;
using System.IO;

namespace Statr.Server.Storage.Engine
{
    public class StorageTree : IStorageTree
    {
        public StorageTree(
            string rootFilePath,
            string name)
        {
            Name = name;
            FilePath = Path.Combine(rootFilePath, Name);
        }

        public string Name { get; set; }

        public string FilePath { get; private set; }

        public void Initialize()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }

        public IStorageNode GetOrCreateNode(string name)
        {
            return GetOrCreateNode(name, c => { });
        }

        public IStorageNode GetOrCreateNode(string name, Action<IStorageNodeConfiguration> configuration)
        {
            var nodeConfiguration = StorageNodeConfiguration.Default;
            configuration(nodeConfiguration);

            var storageNode = new StorageNode(this, name, nodeConfiguration);
            storageNode.Initialize();
            return storageNode;
        }

        public void DeleteAllNodes()
        {
            var info = new DirectoryInfo(FilePath);
            foreach (var i in info.GetDirectories())
            {
                i.Delete(true);
            }
        }
    }
}