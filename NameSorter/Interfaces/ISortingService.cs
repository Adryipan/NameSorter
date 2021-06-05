using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    public interface ISortingService
    {
        /// <summary>
        /// This method should short the list of names in the given file by last names, 
        /// write result to text file as specified and return the sorted result
        /// </summary>
        /// <param name="path">File that contains names to be read and sorted</param>
        /// <param name="outputFileName">Destination path for writing the result</param>
        /// <returns>A string array contains a sorted list of names</returns>
        string[] SortByLastName(string path, string outputFileName);
    }
}
