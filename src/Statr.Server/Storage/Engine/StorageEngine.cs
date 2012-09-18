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
            Namespace = "default";

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public string RootFilePath { get; set; }

        public string Namespace { get; set; }

        public IStorageTree GetOrCreateTree(string name)
        {
            return GetOrCreateTree(name, c => { });
        }

        public IStorageTree GetOrCreateTree(string name, Action<IStorageTreeConfiguration> configuration)
        {
            Logger.DebugFormat("Creating storage tree: {0}", name);

            EnsureAllPropertiesAreConfigured();

            var treeConfiguration = StorageTreeConfiguration.Default;
            configuration(treeConfiguration);

            var storageTree = new StorageTree(
                RootFilePath,
                name);

            storageTree.Initialize();

            return storageTree;
        }

        public IEnumerable<BucketReference> ListBuckets()
        {
            EnsureAllPropertiesAreConfigured();

            var combine = Path.Combine(RootFilePath, Namespace);
            var info = new DirectoryInfo(combine);

            if (!info.Exists)
            {
                yield break;
            }

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

        public IDataPointWriter GetWriter(BucketReference bucketReference)
        {
            var storageTreeName = string.Concat(Namespace, "/", bucketReference.MetricType.ToString().ToLower());
            var storageTree = GetOrCreateTree(storageTreeName);
            return new StorageEngineDataPointWriter(storageTree, bucketReference);
        }

        public void NotifyConfigChanged(Config config)
        {
            RootFilePath = config.Directory;
            Namespace = config.Namespace;
        }

        private void EnsureAllPropertiesAreConfigured()
        {
            if (string.IsNullOrEmpty(RootFilePath))
            {
                throw new StatrException(
                    "Cannot create a storage engine tree because the RootFilePath has not been specified");
            }

            if (string.IsNullOrEmpty(Namespace))
            {
                throw new StatrException(
                    "Cannot create a storage engine tree because a Namespace has not been specified");
            }
        }
    }
}
