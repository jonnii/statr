using Machine.Specifications;
using Statr.Server.Configuration;

namespace Statr.Server.Specifications.Configuration
{
    public class BufferConfigSpecification
    {
        [Subject(typeof(BufferConfig))]
        public class default_storage
        {
            Establish context = () =>
                config = BufferConfig.Default;

            It should_be_buffered_storage = () =>
                config.Type.ShouldEqual("BufferedStrategy");

            static BufferConfig config;
        }
    }
}
