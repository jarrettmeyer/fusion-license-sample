using System;
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace LicenseSample
{
    [TestFixture]
    public class BetterLicense_Tests
    {
        private IClock clock;
        private Mock<IFileSystem> fileSystem;
        private ILicense license;
        private Mock<IRegistrySettings> registrySettings;

        [SetUp]
        public void before_each_test()
        {
            fileSystem = new Mock<IFileSystem>();
            registrySettings = new Mock<IRegistrySettings>();
        }

        [Test]
        public void license_should_implement_interface()
        {
            typeof(ILicense).IsAssignableFrom(typeof(BetterLicense)).Should().BeTrue();
        }

        [Test]
        public void can_create_new_instance_using_registry()
        {
            clock = StubClock.ForDateTime(2012, 10, 11, 11, 59, 59, 999);
            SetupRegistryToReturnDate(new DateTime(2012, 10, 11, 12, 0, 0));
            license = new BetterLicense(clock, registrySettings.Object, fileSystem.Object);
            license.Should().BeOfType<BetterLicense>();
        }

        [Test]
        public void can_create_new_instance_using_file()
        {
            clock = StubClock.ForDateTime(2012, 10, 11, 11, 59, 59, 999);
            SetupFileToReturnDate(new DateTime(2012, 10, 11, 12, 0, 0));
            new BetterLicense(clock, registrySettings.Object, fileSystem.Object).Should().BeOfType<BetterLicense>();
        }

        [Test]
        public void license_from_file_should_be_valid()
        {
            clock = StubClock.ForDateTime(2012, 10, 11, 11, 59, 59);
            SetupFileToReturnDate(new DateTime(2012, 10, 11, 12, 0, 0));
            new BetterLicense(clock, registrySettings.Object, fileSystem.Object).IsValid.Should().BeTrue();
        }

        [Test]
        public void license_from_registry_should_be_valid()
        {
            clock = StubClock.ForDateTime(2012, 10, 11, 12, 0, 0);
            SetupRegistryToReturnDate(new DateTime(2012, 10, 11, 12, 0, 1));
            new BetterLicense(clock, registrySettings.Object, fileSystem.Object).IsValid.Should().BeTrue();
        }

        [Test]
        public void license_from_file_should_be_deleted()
        {
            clock = StubClock.ForDateTime(2012, 10, 11, 12, 0, 0);
            SetupFileToReturnDate(new DateTime(2012, 10, 11, 12, 0, 1));
            new BetterLicense(clock, registrySettings.Object, fileSystem.Object);
            fileSystem.Verify(x => x.DeleteFile(License.FILE_PATH));
        }

        [Test]
        public void throws_exception_when_license_is_not_valid()
        {
            Action action = () => { var x = new BetterLicense(clock, registrySettings.Object, fileSystem.Object).IsValid; };
            action.ShouldThrow<LicenseKeyException>();
        }

        [Test]
        public void throws_exception_when_no_license_exists()
        {
            Action action = () => new BetterLicense(clock, registrySettings.Object, fileSystem.Object);
            action.ShouldThrow<LicenseKeyException>();
        }

        [Test]
        public void what_happens_now()
        {
            var counter = 0;
            for (var i = 0; i < 10000; i++)
            {
                try
                {
                    clock = StubClock.ForDateTime(2012, 10, 11, 11, 59, 59, 999);
                    SetupRegistryToReturnDate(new DateTime(2012, 10, 11, 12, 0, 0));
                    if (new BetterLicense(clock, registrySettings.Object, fileSystem.Object).IsValid)
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

        private void SetupRegistryToReturnDate(DateTime dateTime)
        {
            registrySettings.Setup(x => x.HasKey(License.REGISTRY_PATH)).Returns(true);
            registrySettings.Setup(x => x.ReadKey(License.REGISTRY_PATH)).Returns(dateTime.ToString());
        }

        private void SetupFileToReturnDate(DateTime dateTime)
        {
            var bytes = Encoding.ASCII.GetBytes(dateTime.ToString());
            var base64content = Convert.ToBase64String(bytes);

            fileSystem.Setup(x => x.DoesFileExist(License.FILE_PATH)).Returns(true);
            fileSystem.Setup(x => x.ReadAllFileText(License.FILE_PATH)).Returns(base64content);
        }
    }
}