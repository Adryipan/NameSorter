using System;
using System.Linq;

namespace NameSorter
{
    class SortingService : ISortingService
    {
        private readonly IIOService _ioService;

        public SortingService(IIOService ioService)
        {
            _ioService = ioService;
        }
        public string[] SortByLastName(string path)
        {
            //Retrieve data for sorting
            var nameList = _ioService.ReadFromTextFile(path);

            // Rearrange the name to put the last name to the start of the name
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = RearrangeLastName(nameList[i], -1);
            }
                
            Array.Sort(nameList);

            // Revert the name
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = RearrangeLastName(nameList[i], 0);
            }

            _ioService.WriteToTextFile("sorted-names-list.txt", nameList);           

            return nameList;
        }

        private string RearrangeLastName(string name, int lastNameIndex)
        {
            //Split the name into names
            var splittedName = name.Split(' ').ToList();
            //Insert the last name into the start of the array 
            if (lastNameIndex == -1)
            {
                lastNameIndex = splittedName.Count - 1;
                string lastName = splittedName[lastNameIndex];
                splittedName.RemoveAt(lastNameIndex);
                splittedName.Insert(0, lastName);
            }
            else // Revert the name
            {
                string lastName = splittedName[lastNameIndex];
                splittedName.RemoveAt(lastNameIndex);
                splittedName.Add(lastName);
            }
            

            return String.Join(" ", splittedName.ToArray());
        }
    }
}
