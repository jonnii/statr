using System;
using System.IO;
using NUnit.Framework;
using Statr.Server;
using Statr.Server.Storage;
using Statr.Server.Storage.Engine;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheStorageEngine
    {
        public static Action<StatrContext> Executes(Action<IStorageEngine> storageAction)
        {
            return context => storageAction(context.StorageEngine);
        }

        public static Action<StatrContext> ShouldHaveCreatedDirectory(string directory)
        {
            return context =>
            {
                var rootPath = ((StorageEngine)context.StorageEngine).RootFilePath;
                var fullDirectoryPath = Path.Combine(rootPath, directory);

                Assert.That(
                    Directory.Exists(fullDirectoryPath),
                    "Expected the storage engine to have created the directory: {0}", fullDirectoryPath);
            };
        }

        public static Action<StatrContext> WritesDataPoints(string bucketName, MetricType metricType, params DataPoint[] points)
        {
            return context =>
            {
                var storageEngine = context.StorageEngine;
                var writer = storageEngine.GetWriter(new BucketReference(bucketName, metricType));
                writer.Write(points);
            };
        }
    }
}
