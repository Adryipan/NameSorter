using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    interface IInputService
    {
        string[] ReadFromTextFile(string path);
    }
}
