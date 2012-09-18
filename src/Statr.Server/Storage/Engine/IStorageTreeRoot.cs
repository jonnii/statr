using System;

namespace Statr.Server.Storage.Engine
{
    /// <summary>
    /// The storage tree root gives you access to storage trees
    /// </summary>
    public interface IStorageTreeRoot
    {
        /// <summary>
        /// Gets or creates a tree by name
        /// </summary>
        IStorageTree GetOrCreateTree(string name);

        /// <summary>
        /// Gets or creates a tree by name with custom configuration
        /// </summary>
        IStorageTree GetOrCreateTree(string name, Action<IStorageTreeConfiguration> configuration);
    }
}