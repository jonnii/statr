using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Statr.Storage.Strategies
{
    public class ImmediateStorageStrategy : IStorageStrategy
    {
        public IObservable<IEnumerable<DataPointEvent>> Apply(IObservable<DataPointEvent> dataPointEvents)
        {
            return dataPointEvents.Buffer(1);
        }
    }
}
