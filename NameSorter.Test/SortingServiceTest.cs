using System.IO;
using Moq;
using NUnit.Framework;

namespace NameSorter.Test
{
    public class SortingServiceTest
    {
        private Mock<IIOService> mockIOService;
        private string BASE_PATH;

        [SetUp]
        public void Setup()
        {
            mockIOService = new Mock<IIOService>();
            BASE_PATH = Directory.GetCurrentDirectory();
        }

        [Test]
        public void ShouldReturnEmptyStringArray()
        {
            string readPath = Path.Combine(BASE_PATH, "empty.txt");
            string savePath = Path.Combine(BASE_PATH, "result.txt");
            string[] expectedContent = { };
            mockIOService.Setup(m => m.ReadFromTextFile(readPath))
                .Returns(expectedContent);
            mockIOService.Setup(m => m.WriteToTextFile(savePath, expectedContent));

            var sortingService = new SortingService(mockIOService.Object);

            var actualConten = sortingService.SortByLastName(readPath, savePath);

            Assert.AreEqual(expectedContent, actualConten);
        }

        [Test]
        public void ShouldSortOneNameByLastName_ReturnSortedNameAsArray()
        {
            string readPath = Path.Combine(BASE_PATH, "oneName.txt");
            string savePath = Path.Combine(BASE_PATH, "result.txt");
            string[] expectedContent = {"John Smith"};
            mockIOService.Setup(m => m.ReadFromTextFile(readPath))
                .Returns(expectedContent);
            mockIOService.Setup(m => m.WriteToTextFile(savePath, expectedContent));

            var sortingService = new SortingService(mockIOService.Object);

            var actualConten = sortingService.SortByLastName(readPath, savePath);

            Assert.AreEqual(expectedContent, actualConten);
        }

        [Test]
        public void ShouldSortMultipleNameByLastName_ReturnSortedNameAsArray()
        {
            string readPath = Path.Combine(BASE_PATH, "multipleName.txt");
            string savePath = Path.Combine(BASE_PATH, "result.txt");
            string[] fileContent = { "Janet Parsons", "Vaughn Lewis",  "Adonis Julius Archer", "Marin Alvarez", "Iunter Uriah Mathew Clarke", "Hunter Uriah Mathew Clarke"};
            string[] expectedContent = { "Marin Alvarez", "Adonis Julius Archer", "Hunter Uriah Mathew Clarke", "Iunter Uriah Mathew Clarke", "Vaughn Lewis", "Janet Parsons"};


            mockIOService.Setup(m => m.ReadFromTextFile(readPath))
                .Returns(fileContent);
            mockIOService.Setup(m => m.WriteToTextFile(savePath, expectedContent));

            var sortingService = new SortingService(mockIOService.Object);
            
            var actualConten = sortingService.SortByLastName(readPath, savePath);

            Assert.AreEqual(expectedContent, actualConten);
        }

        [Test]
        public void ShouldIgnoreCase_SortMultipleNameByLastName_ReturnSortedNameAsArray()
        {
            string readPath = Path.Combine(BASE_PATH, "multipleName.txt");
            string savePath = Path.Combine(BASE_PATH, "result.txt");
            string[] fileContent = { "Hunter Uriah Mathew Clarke", "Hunter Uriah Mathew clarke" };
            string[] expectedContent = { "Hunter Uriah Mathew clarke", "Hunter Uriah Mathew Clarke"};


            mockIOService.Setup(m => m.ReadFromTextFile(readPath))
                .Returns(fileContent);
            mockIOService.Setup(m => m.WriteToTextFile(savePath, expectedContent));

            var sortingService = new SortingService(mockIOService.Object);

            var actualContent = sortingService.SortByLastName(readPath, savePath);
            
            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}