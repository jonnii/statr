using System;

namespace Statr.Storage
{
    public interface IStorageTree
    {
        string FilePath { get; }

        IStorageNode GetOrCreateNode(string node);

        IStorageNode GetOrCreateNode(string node, Action<IStorageNodeConfiguration> action);
    }
}