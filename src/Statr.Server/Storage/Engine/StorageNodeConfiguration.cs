namespace Statr.Server.Storage.Engine
{
    public class StorageNodeConfiguration : IStorageNodeConfiguration
    {
        public int TimeStep { get; set; }

        public static IStorageNodeConfiguration Default
        {
            get
            {
                return new StorageNodeConfiguration
                {
                    TimeStep = 60
                };
            }
        }
    }
}