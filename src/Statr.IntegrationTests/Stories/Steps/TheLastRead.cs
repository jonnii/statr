using System;
using System.Collections.Generic;
using NUnit.Framework;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheLastRead
    {
        public static Action<StatrContext> ShouldBe(Predicate<IEnumerable<DataPoint>> predicate)
        {
            return context =>
            {
                var points = context.Get<IEnumerable<DataPoint>>();
                Assert.That(predicate(points), "Last read did not pass predicate");
            };
        }
    }
}