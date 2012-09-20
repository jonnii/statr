using System;
using System.Reactive.Subjects;
using Castle.Core.Logging;

namespace Statr.Server.Routing
{
    public class DataPointStream : IDataPointStream, IDisposable
    {
        private readonly Subject<DataPointEvent> root;

        public DataPointStream()
        {
            root = new Subject<DataPointEvent>();

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IObservable<DataPointEvent> DataPoints
        {
            get { return root; }
        }

        public void Register(IDataPointGenerator dataPointGenerator)
        {
            Logger.DebugFormat("Registering data point generator");

            var observable = dataPointGenerator.DataPoints;
            observable.Subscribe(c => root.OnNext(c));
        }

        public void Dispose()
        {
            Logger.InfoFormat("Disposing DataPointStream");

            root.OnCompleted();
            root.Dispose();
        }
    }
}