using System;

namespace Statr.Storage
{
    public interface IStorageEngine
    {
        string RootFilePath { get; }

        IStorageTree GetOrCreateTree(string name);

        IStorageTree GetOrCreateTree(string name, Action<IStorageTreeConfiguration> configuration);
    }
}