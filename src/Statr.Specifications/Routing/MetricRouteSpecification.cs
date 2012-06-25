using System.Threading;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouteSpecification
    {
        [Subject(typeof(MetricRoute))]
        public class when_pushing : with_route
        {
            Establish context = () =>
            {
                Subject.Start();
                Subject.DataPointGenerated += (o, e) => dataPoint = e.DataPoint;
            };

            Because of = () =>
            {
                Subject.Push(new CountMetric("asdf", 5));
                Thread.Sleep(2000);
            };

            It should_create_data_point = () =>
                dataPoint.Value.ShouldEqual(5);

            static DataPoint dataPoint;
        }

        public class with_route
        {
            Establish context = () =>
                Subject = new MetricRoute("key");

            protected static MetricRoute Subject;
        }
    }
}
