using System;

namespace LicenseSample
{
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

        /// <summary>
        /// This method is an example of a factory pattern. I would strongly encourage you to create
        /// very explicit factory methods for building your objects. They greatly increase readability
        /// and let you explictly return interfaces instead of the actual class the way a constructor
        /// would.
        /// </summary>
        public static IClock ForDateTime(int year, int month, int day, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
        {
            return new StubClock(new DateTime(year, month, day, hours, minutes, seconds, milliseconds));
        }
    }
}
