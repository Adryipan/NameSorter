using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace NameSorter.Test
{
    class Program
    {
        private string testFilePath;
        private string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");

        [SetUp]
        public void Setup()
        {
            string[] names = {"Janet Parsons","Vaughn Lewis","Adonis Julius Archer",
                "Shelby Nathan Yoder","Marin Alvarez","London Lindsey", "Beau Tristan Bentley",
                "Leo Gardner","Hunter Uriah Mathew Clarke","Mikayla Lopez", "Frankie Conner Ritter"};
            testFilePath = Path.Combine(Directory.GetCurrentDirectory(), "unsorted-names.txt");
            File.WriteAllLines(testFilePath, names);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testFilePath);
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
        }

        [Test]
        public void ShouldPrintSortedNamesAndWriteToTxt_ReturnZero()
        {
            int CODE_OK = 0;
            Process process = StartConsoleApplciation(new string[] { testFilePath });


            process.Start();
            process.WaitForExit();

            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.WriteLine(process.StandardError.ReadToEnd());

            int exitCode = process.ExitCode;

            Assert.AreEqual(CODE_OK, exitCode);
            Assert.IsTrue(File.Exists(outputFilePath));

            File.Delete(outputFilePath);
        }

        [Test]
        public void ShouldRequestForArgument_ReturnOne()
        {
            int CODE_ERROR = 1;
            Process process = StartConsoleApplciation(new string[] { });
            string actualStdout;
            string expectedStdout = "Please provide a file as an argument.";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                process.Start();
                process.WaitForExit();
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                actualStdout = sw.ToString().Replace("\n", "").Replace("\r", "");
            }

            int exitCode = process.ExitCode;

            Assert.AreEqual(CODE_ERROR, exitCode);
            Assert.AreEqual(expectedStdout, actualStdout);
            Assert.IsFalse(File.Exists(outputFilePath));

        }

        [Test]
        public void ShouldRequestForOnlyOneArgument_ReturnOne()
        {
            int CODE_ERROR = 1;
            Process process = StartConsoleApplciation(new string[] { testFilePath, testFilePath });
            string actualStdout;
            string expectedStdout = "Please provide only one argument.";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                process.Start();
                process.WaitForExit();
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                actualStdout = sw.ToString().Replace("\n", "").Replace("\r", "");
            }

            int exitCode = process.ExitCode;

            Assert.AreEqual(CODE_ERROR, exitCode);
            Assert.AreEqual(expectedStdout, actualStdout);
            Assert.IsFalse(File.Exists(outputFilePath));

        }

        [Test]
        public void ShouldRequestForValidFile_ReturnOne()
        {
            int CODE_ERROR = 1;
            Process process = StartConsoleApplciation(new string[] { testFilePath + "abc" });
            string actualStdout;
            string expectedStdout = "File does not exist.";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                process.Start();
                process.WaitForExit();
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                actualStdout = sw.ToString().Replace("\n", "").Replace("\r","");
            }

            int exitCode = process.ExitCode;

            Assert.AreEqual(CODE_ERROR, exitCode);
            Assert.AreEqual(expectedStdout, actualStdout);
            Assert.IsFalse(File.Exists(outputFilePath));

        }

        private Process StartConsoleApplciation(string[] args)
        {
            string arguments = string.Join(" ", args);

            Process process = new Process();

            process.StartInfo = new ProcessStartInfo
            {
                FileName = "NameSorter",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            return process;
        }
    }
}
