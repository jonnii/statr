using System;
using System.Runtime.Serialization;

namespace Statr
{
    [Serializable]
    public class StatrException : Exception
    {
        public StatrException()
        {

        }

        public StatrException(string message)
            : base(message)
        {

        }

        public StatrException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected StatrException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}