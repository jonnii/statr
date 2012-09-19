using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Storage.Engine;

namespace Statr.Server.Specifications.Storage.Engine
{
    public class StorageSliceSpecification
    {
        [Subject(typeof(StorageSlice))]
        public class when_creating_from_filename : WithFakes
        {
            Because of = () =>
                slice = new StorageSlice(An<IStorageNode>(), "12345@50.slice");

            It should_set_time_step = () =>
                slice.TimeStep.ShouldEqual(50);

            It should_set_start_time = () =>
                slice.StartTime.ShouldEqual(12345);

            static StorageSlice slice;
        }
    }
}
