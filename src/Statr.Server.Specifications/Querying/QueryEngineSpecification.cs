using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Querying;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Querying
{
    public class QueryEngineSpecification
    {
        [Subject(typeof(QueryEngine))]
        public class when : WithSubject<QueryEngine>
        {
            Because of = () =>
                Subject.Execute(new Query("bucket", MetricType.Count));

            It should_fetch_datapoints_from_cache = () =>
                The<IDataPointCache>().WasToldTo(c => c.Get(new BucketReference("bucket", MetricType.Count)));
        }
    }
}
