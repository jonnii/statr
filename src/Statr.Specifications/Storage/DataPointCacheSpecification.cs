using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;
using Statr.Storage;

namespace Statr.Specifications.Storage
{
    public class DataPointCacheSpecification
    {
        [Subject(typeof(DataPointCache))]
        public class with_empty_cache : WithSubject<DataPointCache>
        {
            Because of = () =>
                points = Subject.Get(new RouteKey("metric.name", MetricType.Count));

            It should_get_empty_result = () =>
                points.ShouldBeEmpty();

            static IEnumerable<DataPoint> points;
        }

        [Subject(typeof(DataPointCache))]
        public class with_points : WithSubject<DataPointCache>
        {
            Establish context = () =>
                Subject.Push(new RouteKey("metric.name", MetricType.Count), new DataPoint(DateTime.Now, 500));

            Because of = () =>
                points = Subject.Get(new RouteKey("metric.name", MetricType.Count));

            It should_get_results = () =>
                points.ShouldNotBeEmpty();

            static IEnumerable<DataPoint> points;
        }
    }
}
