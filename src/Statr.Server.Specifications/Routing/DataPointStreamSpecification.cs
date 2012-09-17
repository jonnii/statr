using System;
using System.Reactive.Subjects;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class DataPointStreamSpecification
    {
        [Subject(typeof(DataPointStream))]
        public class when_events_raised_on_data_point_generators : with_source
        {
            Because of = () =>
            {
                var args = new DataPointEvent(
                    new BucketReference("name", MetricType.Count),
                    new DataPoint(DateTime.Now, 500, 1));

                Subject.DataPoints.Subscribe(d => ++raised);

                dataPoints1.OnNext(args);
                dataPoints2.OnNext(args);
                dataPoints1.OnNext(args);
            };

            It should_combine_all_events = () =>
                raised.ShouldEqual(3);

            static int raised;
        }

        public class with_source
        {
            Establish context = () =>
            {
                dataPoints1 = new Subject<DataPointEvent>();
                dataPoints2 = new Subject<DataPointEvent>();

                generator1 = new Moq.Mock<IDataPointGenerator>();
                generator1.Setup(g => g.DataPoints).Returns(dataPoints1);

                generator2 = new Moq.Mock<IDataPointGenerator>();
                generator2.Setup(g => g.DataPoints).Returns(dataPoints2);

                Subject = new DataPointStream();
                Subject.Register(generator1.Object);
                Subject.Register(generator2.Object);
            };

            protected static DataPointStream Subject;

            protected static Moq.Mock<IDataPointGenerator> generator1;

            protected static Moq.Mock<IDataPointGenerator> generator2;

            protected static Subject<DataPointEvent> dataPoints1;

            protected static Subject<DataPointEvent> dataPoints2;
        }
    }
}
