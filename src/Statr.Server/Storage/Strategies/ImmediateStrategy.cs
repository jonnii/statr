using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Statr.Server.Storage.Strategies
{
    public class ImmediateStrategy : IBufferStrategy
    {
        public IObservable<IEnumerable<DataPoint>> Apply(IObservable<DataPoint> dataPoints)
        {
            return dataPoints.Buffer(1);
        }
    }
}
