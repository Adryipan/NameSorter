using System;
using System.IO;


namespace NameSorter
{
    class IOService : IIOService
    {
        /// <summary>
        /// Open and read in the specified file
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>An array of string that contains all contents of the specified file</returns>
        public string[] ReadFromTextFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public bool WriteToTextFile(string path, string[] content)
        {

            return true;
        }
    }
}
