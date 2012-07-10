using Machine.Specifications;
using Statr.Configuration;

namespace Statr.Specifications.Configuration
{
    public class StorageConfigSpecification
    {
        [Subject(typeof(StorageConfig))]
        public class default_storage
        {
            Establish context = () =>
                config = StorageConfig.Default;

            It should_be_buffered_storage = () =>
                config.Type.ShouldEqual("BufferedStorageStrategy");

            static StorageConfig config;
        }
    }
}
