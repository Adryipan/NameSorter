using System;
using System.IO;


namespace NameSorter
{
    class InputService : IInputService
    {
        public string[] ReadFromTextFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
