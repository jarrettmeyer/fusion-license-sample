using System;

namespace LicenseSample
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}