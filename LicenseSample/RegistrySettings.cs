using System;
using Microsoft.Win32;

namespace LicenseSample
{
    public interface IRegistrySettings
    {
        bool HasKey(string path);
        string ReadKey(string path);
        void WriteKey(string path, string value);
    }

    public class RegistrySettings : IRegistrySettings
    {
        public bool HasKey(string path)
        {
            return GetValue(path) != null;
        }

        private static object GetValue(string path)
        {
            return Registry.LocalMachine.GetValue(path);
        }

        public string ReadKey(string path)
        {
            return HasKey(path) ? Convert.ToString(GetValue(path)) : null;
        }

        public void WriteKey(string path, string value)
        {
            Registry.LocalMachine.SetValue(path, value);
        }
    }
}
