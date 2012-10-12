using System;
using System.Text;

namespace LicenseSample
{
    public class BetterLicense : ILicense
    {
        private readonly IClock clock;
        private DateTime? expiration;
        private readonly IFileSystem fileSystem;
        private readonly IRegistrySettings registrySettings;

        public BetterLicense(IClock clock, IRegistrySettings registrySettings, IFileSystem fileSystem)
        {
            this.clock = clock;
            this.registrySettings = registrySettings;
            this.fileSystem = fileSystem;

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
                var isValid = clock.Now < expiration;
                if (isValid)
                    return true;

                throw LicenseKeyException.Expired;
            }
        }

        private void DeleteFile()
        {
            fileSystem.DeleteFile(License.FILE_PATH);
        }

        private void GetExpirationFromFileSystem()
        {
            if (fileSystem.DoesFileExist(License.FILE_PATH))
            {
                var text = fileSystem.ReadAllFileText(License.FILE_PATH);
                var bytes = Convert.FromBase64String(text);
                var expirationString = Encoding.ASCII.GetString(bytes);
                expiration = DateTime.Parse(expirationString);
            }
        }

        private void GetExpirationFromRegistry()
        {
            string value = registrySettings.ReadKey(License.REGISTRY_PATH);
            if (value != null)
            {
                expiration = DateTime.Parse(value);
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
            registrySettings.WriteKey(License.REGISTRY_PATH, expiration.ToString());
        }
    }
}