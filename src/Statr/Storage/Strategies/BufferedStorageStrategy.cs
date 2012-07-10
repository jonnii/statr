using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Statr.Storage.Strategies
{
    public class BufferedStorageStrategy : IStorageStrategy
    {
        public BufferedStorageStrategy()
        {
            BufferSize = 100;
        }

        public int BufferSize { get; set; }

        public IObservable<IEnumerable<DataPoint>> Apply(IObservable<DataPoint> dataPointEvents)
        {
            return dataPointEvents.Buffer(BufferSize);
        }
    }
}