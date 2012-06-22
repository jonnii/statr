using System;

namespace Statr.Client
{
    /// <summary>
    /// The statr client is used to communicate with the
    /// statr server
    /// </summary>
    public interface IStatrClient : IDisposable
    {
        /// <summary>
        /// Increments the given bucket by a single unit
        /// </summary>
        /// <param name="bucket">The bucket to increment</param>
        void Count(string bucket);

        /// <summary>
        /// Increments the given bucket by the specified amount
        /// </summary>
        /// <param name="bucket">The bucket to increment</param>
        /// <param name="amount">The amount to increment by</param>
        void Count(string bucket, int amount);

        /// <summary>
        /// Sets the gauge to the specified value
        /// </summary>
        /// <param name="gaugeName">The name of the gauge</param>
        /// <param name="value">The value to increment by</param>
        void Gauge(string gaugeName, int value);
    }
}