namespace Statr.Server.Storage.Engine
{
    public class StorageTreeConfiguration : IStorageTreeConfiguration
    {
        public static IStorageTreeConfiguration Default
        {
            get { return new StorageTreeConfiguration(); }
        }
    }
}