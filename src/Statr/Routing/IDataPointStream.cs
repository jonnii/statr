using System;

namespace Statr.Routing
{
    public interface IDataPointStream
    {
        IObservable<DataPointEvent> DataPoints { get; }

        void Register(IDataPointGenerator dataPointGenerator);
    }
}
