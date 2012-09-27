using System;

namespace LicenseSample
{
    public interface IClock
    {
        DateTime Now { get; }
    }

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

    public sealed class StubClock : IClock
    {
        private readonly DateTime stub;

        private StubClock(DateTime stub)
        {
            this.stub = stub;
        }

        public DateTime Now
        {
            get { return stub; }
        }

        public static IClock ForDateTime(DateTime dateTime)
        {
            return new StubClock(dateTime);
        }
    }
}
