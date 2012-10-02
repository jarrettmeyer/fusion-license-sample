using System;
using System.IO;

namespace LicenseSample
{
    public interface IFileSystem
    {
        void DeleteFile(string path);

        bool DoesFileExist(string path);

        string ReadAllFileText(string path);
    }

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

    public class FakeFileSystem : IFileSystem
    {
        public FakeFileSystem()
        {
            OnDeleteFile = s => { };
            OnDoesFileExist = s => true;
            OnReadAllFileText = s => "Hello, World!";
        }

        public Action<string> OnDeleteFile { get; set; }
        public Func<string, bool> OnDoesFileExist { get; set; }
        public Func<string, string> OnReadAllFileText { get; set; }

        public void DeleteFile(string path)
        {
            OnDeleteFile(path);
        }

        public bool DoesFileExist(string path)
        {
            return OnDoesFileExist(path);
        }

        public string ReadAllFileText(string path)
        {
            return OnReadAllFileText(path);
        }
    }
}
