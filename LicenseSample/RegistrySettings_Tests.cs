using FluentAssertions;
using NUnit.Framework;

namespace LicenseSample
{
    [TestFixture]
    public class RegistrySettings_Tests
    {
        private const string myKey = @"MY_TEST_KEY";
        private IRegistrySettings registrySettings;

        [SetUp]
        public void before_each_test()
        {
            registrySettings = new LocalMachineRegistrySettings();
        }

        [TearDown]
        public void after_each_test()
        {
            DeleteRegistryValue();
        }

        [Test]
        public void can_set_a_value()
        {
            registrySettings.WriteKey(myKey, "something awesome");
            var storedValue = GetRegistryValue();
            storedValue.Should().Be("something awesome");
        }

        [Test]
        public void can_read_set_value()
        {
            const string someValue = "this is a test setting";
            SetRegistryValue(someValue);
            registrySettings.ReadKey(myKey).Should().Be(someValue);
        }

        [Test]
        public void returns_true_when_value_is_set()
        {
            registrySettings.WriteKey(myKey, "abcdefg");
            registrySettings.HasKey(myKey).Should().BeTrue();
        }

        [Test]
        public void returns_false_when_value_is_not_set()
        {
            registrySettings.HasKey(myKey).Should().BeFalse();
        }

        private void DeleteRegistryValue()
        {
            if (Microsoft.Win32.Registry.LocalMachine.GetValue(myKey) != null)
                Microsoft.Win32.Registry.LocalMachine.DeleteValue(myKey);
        }

        private void SetRegistryValue(string someValue)
        {
            Microsoft.Win32.Registry.LocalMachine.SetValue(myKey, someValue);
        }

        private object GetRegistryValue()
        {
            return Microsoft.Win32.Registry.LocalMachine.GetValue(myKey);
        }
    }
}
