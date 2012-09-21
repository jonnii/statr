using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Statr.Server.Storage.Strategies
{
    public class BufferedStrategy : IBufferStrategy
    {
        public BufferedStrategy()
        {
            BufferSize = 100;
        }

        public int BufferSize { get; set; }

        public IObservable<IEnumerable<DataPoint>> Apply(IObservable<DataPoint> dataPoints)
        {
            return dataPoints.Buffer(BufferSize);
        }

        public override string ToString()
        {
            return string.Format("[BufferedStrategy BufferSize={0}]", BufferSize);
        }
    }
}