using System;
using System.IO;
using System.Text;

namespace LicenseSample
{
    public interface ILicense
    {
        bool IsValid { get; }
    }

    public class License : ILicense
    {
        public const string FILE_PATH = @"C:\temp\license.txt";
        public const string REGISTRY_PATH = @"SOFTWARE\Fusion Alliance\License Sample\Expiration";

        private DateTime? expiration;

        public License()
        {
            Initialize();
        }

        /// <summary>
        /// This accessor is the only real business rule in the application. If the
        /// current date is less than the expiration date, then the license is valid
        /// and should return true. Otherwise, throw an exception.
        /// </summary>
        public bool IsValid
        {
            get
            {
                var isValid = DateTime.Now < expiration;
                if (isValid)
                    return true;

                throw LicenseKeyException.Expired;
            }
        }

        private void DeleteFile()
        {
            File.Delete(FILE_PATH);
        }

        private void GetExpirationFromFileSystem()
        {
            if (File.Exists(FILE_PATH))
            {
                var text = File.ReadAllText(FILE_PATH);
                var bytes = Convert.FromBase64String(text);
                var expirationString = Encoding.ASCII.GetString(bytes);
                expiration = DateTime.Parse(expirationString);
            }
        }

        private void GetExpirationFromRegistry()
        {
            object value = Microsoft.Win32.Registry.LocalMachine.GetValue(REGISTRY_PATH);
            if (value != null)
            {
                expiration = DateTime.Parse(value.ToString());
            }
        }

        private void Initialize()
        {
            GetExpirationFromRegistry();

            if (expiration != null)
                return;

            GetExpirationFromFileSystem();

            if (expiration != null)
            {
                SetExpirationInRegistry();
                DeleteFile();
                return;
            }

            throw LicenseKeyException.NotFound;
        }

        private void SetExpirationInRegistry()
        {
            Microsoft.Win32.Registry.LocalMachine.SetValue(REGISTRY_PATH, expiration.ToString());
        }
    }
}
