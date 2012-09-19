using System;
using System.Collections.Generic;
using NUnit.Framework;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheLastQuery
    {
        public static Action<StatrContext> ShouldHave(Predicate<IEnumerable<DataPoint>> resultPredicate)
        {
            return context =>
            {
                var lastResult = context.Get<IEnumerable<DataPoint>>();
                Assert.That(resultPredicate(lastResult), "Last Query Predicate failed =(");
            };
        }
    }
}
