using Machine.Fakes;
using Machine.Specifications;
using Statr.Storage.Strategies;

namespace Statr.Specifications.Storage.Strategies
{
    public class BufferedStorageStrategySpecification
    {
        [Subject(typeof(BufferedStorageStrategy))]
        public class in_general : WithSubject<BufferedStorageStrategy>
        {
            It should_have_default_buffer_size = () =>
                Subject.BufferSize.ShouldEqual(100);
        }
    }
}
