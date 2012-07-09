using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Statr.Storage.Strategies
{
    public class ImmediateStorageStrategy : IStorageStrategy
    {
        public IObservable<IEnumerable<DataPoint>> Apply(IObservable<DataPoint> dataPointEvents)
        {
            return dataPointEvents.Buffer(1);
        }
    }
}
