namespace LicenseSample
{
    /// <summary>
    /// Represents the ability to work with the system registry.
    /// </summary>
    public interface IRegistrySettings
    {
        bool HasKey(string path);
        string ReadKey(string path);
        void WriteKey(string path, string value);
    }
}
