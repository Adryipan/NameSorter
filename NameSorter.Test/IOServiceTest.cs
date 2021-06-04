using System.IO;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace NameSorter.Test
{
    class IOServiceTest
    {
        private IOService _ioService;

        [SetUp]
        public void Setup()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<IOService>();
            _ioService = new IOService(logger);
        }

        [Test]
        public void ShouldReadFile_ReturnStringArray()
        {
            // Generate a file to test read
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "ioReadTest.txt");
            string[] expectedContent = { "Name one", "Name two", "Name three" };
            File.WriteAllLines(savePath, expectedContent);

            var actualResult = _ioService.ReadFromTextFile(savePath);

            Assert.AreEqual(expectedContent, actualResult);

            // Clean up
            File.Delete(savePath);
        }

        [Test]
        public void ShouldWriteFile()
        {
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "ioWriteTest.txt");
            string[] expectedContent = { "Name one", "Name two", "Name three" };
            File.WriteAllLines(savePath, expectedContent);

            Assert.IsTrue(File.Exists(savePath));

            File.Delete(savePath);
        }
    }
}

