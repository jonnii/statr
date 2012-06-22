using System;

namespace Statr.Storage
{
    public interface IStorageEngine
    {
        string RootFilePath { get; }

        IStorageTree CreateTree(string name);

        IStorageTree CreateTree(string name, Action<IStorageTreeConfiguration> configuration);
    }
}