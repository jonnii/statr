using System;
using System.Collections.Generic;

namespace Statr.Storage
{
    public interface IStorageStrategy
    {
        IObservable<IEnumerable<DataPointEvent>> Apply(IObservable<DataPointEvent> dataPoints);
    }
}