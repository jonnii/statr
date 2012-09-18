using System;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Configuration;
using Statr.Server.Storage.Engine;

namespace Statr.Server.Specifications.Storage.Engine
{
    public class StorageEngineSpecification
    {
        [Subject(typeof(StorageEngine))]
        public class when_apply_configuration : WithSubject<StorageEngine>
        {
            Because of = () =>
                Subject.NotifyConfigChanged(new Config { Directory = "swizzle" });

            It should_change_root_path = () =>
                Subject.RootFilePath.ShouldEqual("swizzle");
        }

        [Subject(typeof(StorageEngine))]
        public class when_compacting : WithSubject<StorageEngine>
        {
            Establish context = () =>
                dataPoints = new[]
                {
                    new DataPoint(DateTime.Now.Ticks, 500, 1)    
                };

            static DataPoint[] dataPoints;
        }
    }
}
