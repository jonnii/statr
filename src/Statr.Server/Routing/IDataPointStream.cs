using System;

namespace Statr.Server.Routing
{
    public interface IDataPointStream
    {
        IObservable<DataPointEvent> DataPoints { get; }

        void Register(IDataPointGenerator dataPointGenerator);
    }
}
