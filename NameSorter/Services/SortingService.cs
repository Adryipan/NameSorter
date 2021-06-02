using System;
using System.Linq;

namespace NameSorter
{
    class SortingService : ISortingService
    {
        private readonly IIOService _inputService;

        public SortingService(IIOService inputService)
        {
            _inputService = inputService;
        }
        public string[] SortByLastName(string path)
        {
            //Retrieve data for sorting
            var nameList = _inputService.ReadFromTextFile(path);

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
