using System;

namespace Statr.Storage
{
    public interface IStorageTree
    {
        string FilePath { get; }

        IStorageNode CreateNode(string node);

        IStorageNode CreateNode(string node, Action<IStorageNodeConfiguration> action);
    }
}