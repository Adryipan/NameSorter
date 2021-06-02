using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    interface IIOService
    {
        string[] ReadFromTextFile(string path);
        bool WriteToTextFile(string path, string[] content);
    }
}
