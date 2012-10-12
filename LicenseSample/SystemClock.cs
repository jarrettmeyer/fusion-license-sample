using System;

namespace LicenseSample
{
    public sealed class SystemClock : IClock
    {
        private static readonly SystemClock systemClock = new SystemClock();

        private SystemClock() { }

        /// <summary>
        /// Factory property to return a singleton instance of a <see cref="SystemClock"/>
        /// </summary>
        public static IClock Instance
        {
            get { return systemClock; }
        }

        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
    }
}