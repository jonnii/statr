namespace Statr.Storage
{
    public class StorageTreeConfiguration : IStorageTreeConfiguration
    {
        public static IStorageTreeConfiguration Default
        {
            get { return new StorageTreeConfiguration(); }
        }
    }
}