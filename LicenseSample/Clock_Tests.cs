using System;
using FluentAssertions;
using NUnit.Framework;

namespace LicenseSample
{
    [TestFixture]
    public class Clock_Tests
    {
        [Test]
        public void can_get_system_clock_as_singleton()
        {
            var clock1 = SystemClock.Instance;
            var clock2 = SystemClock.Instance;

            clock1.Should().BeSameAs(clock2);
        }

        [Test]
        public void system_clock_returns_machine_datetime_in_utc()
        {
            SystemClock.Instance.Now.Should().BeWithin(TimeSpan.FromMilliseconds(1));
        }

        [Test]
        public void stubclock_can_be_stubbed_to_return_as_specific_time()
        {
            var stubTime = new DateTime(2012, 9, 27, 8, 15, 0);
            var clock = StubClock.ForDateTime(stubTime);
            clock.Now.Should().Be(stubTime);
        }
    }
}
