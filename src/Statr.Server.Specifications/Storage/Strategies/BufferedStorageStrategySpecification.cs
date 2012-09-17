using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Storage.Strategies;

namespace Statr.Server.Specifications.Storage.Strategies
{
    public class BufferedStorageStrategySpecification
    {
        [Subject(typeof(BufferedStrategy))]
        public class in_general : WithSubject<BufferedStrategy>
        {
            It should_have_default_buffer_size = () =>
                Subject.BufferSize.ShouldEqual(100);
        }
    }
}
