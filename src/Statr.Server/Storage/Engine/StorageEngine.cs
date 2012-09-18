using System;
using System.Collections.Generic;
using System.IO;
using Castle.Core.Logging;
using Statr.Server.Configuration;

namespace Statr.Server.Storage.Engine
{
    public class StorageEngine : IStorageEngine, IConfigWatcher
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

            EnsureRootFilePath();

            var treeConfiguration = StorageTreeConfiguration.Default;
            configuration(treeConfiguration);

            var storageTree = new StorageTree(
                RootFilePath,
                name);

            storageTree.Initialize();

            return storageTree;
        }

        public IEnumerable<BucketReference> ListBuckets(string tree)
        {
            EnsureRootFilePath();

            var combine = Path.Combine(RootFilePath, tree);
            var info = new DirectoryInfo(combine);

            foreach (var metricDirectory in info.GetDirectories())
            {
                var metric = metricDirectory.Name;
                var metricType = MetricTypeParser.Parse(metric);

                foreach (var bucket in metricDirectory.GetDirectories())
                {
                    yield return new BucketReference(bucket.Name, metricType);
                }
            }
        }

        public IDataPointWriter GetWriter(string @namespace, BucketReference bucketReference)
        {
            var storageTreeName = string.Concat(@namespace, "/", bucketReference.MetricType);
            var storageTree = GetOrCreateTree(storageTreeName);
            return new StorageEngineDataPointWriter(storageTree, bucketReference);
        }

        public void NotifyConfigChanged(Config config)
        {
            RootFilePath = config.Directory;
        }

        private void EnsureRootFilePath()
        {
            if (!string.IsNullOrEmpty(RootFilePath))
            {
                return;
            }

            throw new StatrException(
                "Cannot create a storage engine tree because the RootFilePath has not been specified");
        }
    }
}
