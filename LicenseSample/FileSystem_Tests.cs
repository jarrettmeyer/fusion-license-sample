using System;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace LicenseSample
{
    [TestFixture]
    [Category("File System Tests")]
    public class FileSystem_Tests
    {
        private string filePath;
        private IFileSystem fileSystem;

        [SetUp]
        public void before_each_test()
        {
            fileSystem = DefaultFileSystem.Instance;
            CreateJunkFile();
        }

        [TearDown]
        public void after_each_test()
        {
            DeleteJunkFile();
        }

        [Test]
        public void can_delete_a_file_from_disk()
        {
            fileSystem.DeleteFile(filePath);
            File.Exists(filePath).Should().BeFalse();
        }

        [Test]
        public void returns_true_when_file_exists()
        {
            fileSystem.DoesFileExist(filePath).Should().BeTrue();
        }

        [Test]
        public void returns_false_when_file_does_not_exist()
        {
            fileSystem.DoesFileExist(filePath + "2").Should().BeFalse();
        }

        [Test]
        public void can_read_all_file_text()
        {
            var text = fileSystem.ReadAllFileText(filePath);
            text.Should().Be("This is a junk file.");
        }

        public void CreateJunkFile()
        {
            if (!Directory.Exists("C:\\temp"))
                Directory.CreateDirectory("C:\\temp");

            var fileName = Guid.NewGuid().ToString() + ".txt";
            filePath = Path.Combine("C:\\temp", fileName);
            using (var fileStream = File.Create(filePath))
            {
                var buffer = Encoding.ASCII.GetBytes("This is a junk file.");
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }

        public void DeleteJunkFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
