using System.IO;

namespace LicenseSample
{
    public sealed class DefaultFileSystem : IFileSystem
    {
        private readonly static DefaultFileSystem instance = new DefaultFileSystem();

        public static IFileSystem Instance
        {
            get { return instance; }
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllFileText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
