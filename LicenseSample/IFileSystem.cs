namespace LicenseSample
{
    public interface IFileSystem
    {
        void DeleteFile(string path);

        bool DoesFileExist(string path);

        string ReadAllFileText(string path);
    }
}