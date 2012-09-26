using System;

namespace LicenseSample
{
    public class LicenseKeyException : ApplicationException
    {
        private LicenseKeyException(string message)
            : base(message) { }

        public static LicenseKeyException NotFound
        {
            get { return new LicenseKeyException("No license key could be found."); }
        }

        public static LicenseKeyException Expired
        {
            get { return new LicenseKeyException("The license key has expired."); }
        }
    }
}
