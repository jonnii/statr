using System;

namespace Statr.Extensions
{
    public static class EventExtensions
    {
        public static void Raise(this EventHandler<EventArgs> handler, object sender)
        {
            var copy = handler;
            if (copy != null)
            {
                copy(sender, EventArgs.Empty);
            }
        }

        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            var copy = handler;
            if (copy != null)
            {
                copy(sender, args);
            }
        }
    }
}
