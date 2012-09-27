using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace LicenseSample
{
    [TestFixture]
    public class License_Tests
    {
        private static readonly DateTime almostNow = DateTime.Now.AddMilliseconds(1000);
        private readonly static DateTime twoDaysFromNow = DateTime.Today.AddDays(2);
        private static readonly DateTime oneDayAgo = DateTime.Today.AddDays(-1);

        [TearDown]
        public void after_each_test()
        {
            DeleteRegistryValue();
            DeleteFile();
        }

        [Test]
        public void license_should_implement_interface()
        {
            typeof(ILicense).IsAssignableFrom(typeof(License)).Should().BeTrue();
        }

        [Test]
        public void can_create_new_instance_using_registry()
        {
            SetRegistryValue(twoDaysFromNow);
            var license = new License();
            license.Should().BeOfType<License>();
        }

        [Test]
        public void can_create_new_instance_using_file()
        {
            WriteFile(twoDaysFromNow);
            new License().Should().BeOfType<License>();
        }

        [Test]
        public void license_from_file_should_be_valid()
        {
            WriteFile(twoDaysFromNow);
            new License().IsValid.Should().BeTrue();
        }

        [Test]
        public void license_from_registry_should_be_valid()
        {
            SetRegistryValue(twoDaysFromNow);
            new License().IsValid.Should().BeTrue();
        }

        [Test]
        public void license_from_file_should_be_deleted()
        {
            WriteFile(twoDaysFromNow);
            new License();
            File.Exists(License.FILE_PATH).Should().BeFalse();
        }

        [Test]
        public void throws_exception_when_license_is_not_valid()
        {
            SetRegistryValue(oneDayAgo);
            Action action = () => { var x = new License().IsValid; };
            action.ShouldThrow<LicenseKeyException>();
        }

        [Test]
        public void throws_exception_when_no_license_exists()
        {
            Action action = () => new License();
            action.ShouldThrow<LicenseKeyException>();
        }

        [Test, Ignore]
        public void what_happens_now()
        {
            var counter = 0;
            for (var i = 0; i < 10000; i++)
            {
                WriteFile(almostNow);
                try
                {
                    if (new License().IsValid)
                        counter += 1;
                }
                catch (Exception)
                {
                    // swallowed!
                }
                
            }
            Debug.WriteLine("Counter: {0}", counter);
            counter.Should().Be(10000);
        }

        private void DeleteFile()
        {
            if (File.Exists(License.FILE_PATH))
                File.Delete(License.FILE_PATH);
        }

        private void DeleteRegistryValue()
        {
            if (Microsoft.Win32.Registry.LocalMachine.GetValue(License.REGISTRY_PATH) != null)
                Microsoft.Win32.Registry.LocalMachine.DeleteValue(License.REGISTRY_PATH);
        }

        private void SetRegistryValue(DateTime value)
        {
            var registryValue = value.ToString();
            Microsoft.Win32.Registry.LocalMachine.SetValue(License.REGISTRY_PATH, registryValue);
        }

        private void WriteFile(DateTime value)
        {
            if (!Directory.Exists("C:\\temp"))
                Directory.CreateDirectory("C:\\temp");

            using (var fileStream = File.Create(License.FILE_PATH))
            {
                var expirationAsBytes = Encoding.ASCII.GetBytes(value.ToString());
                var base64 = Convert.ToBase64String(expirationAsBytes);

                var base64ByteArray = Encoding.ASCII.GetBytes(base64);
                fileStream.Write(base64ByteArray, 0, base64ByteArray.Length);
            }
        }
    }
}
