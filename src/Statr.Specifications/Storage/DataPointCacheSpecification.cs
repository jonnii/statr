using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Storage;

namespace Statr.Specifications.Storage
{
    public class DataPointCacheSpecification
    {
        [Subject(typeof(DataPointCache))]
        public class when_getting_bucket : with_empty_cache
        {
            Because of = () =>
                points = Subject.Get(new BucketReference("metric.name", MetricType.Count));

            It should_get_empty_result = () =>
                points.ShouldBeEmpty();

            static IEnumerable<DataPoint> points;
        }

        [Subject(typeof(DataPointCache))]
        public class when_getting_bucket_with_points : with_points
        {
            Because of = () =>
                points = Subject.Get(new BucketReference("metric.name", MetricType.Count));

            It should_get_results = () =>
                points.ShouldNotBeEmpty();

            static IEnumerable<DataPoint> points;
        }

        public class with_empty_cache : WithSubject<DataPointCache>
        {

        }

        public class with_points : WithSubject<DataPointCache>
        {
            Establish context = () =>
                Subject.Push(new BucketReference("metric.name", MetricType.Count), new DataPoint(DateTime.Now, 500));
        }
    }
}
