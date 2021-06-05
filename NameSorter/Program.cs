using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NameSorter
{
    class Program
    {
        static int Main(string[] args)
        {
            // Setup dependencies and logger
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddSingleton<ISortingService, SortingService>()
                .AddSingleton<IIOService, IOService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            // Check if one and only one argument is provided. Exit the program if not.
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file as an argument.");
                return 1;
            }
            else if(args.Length > 1)
            {
                Console.WriteLine("Please provide only one argument.");
                return 1;
            }

            // Check if the file exist
            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return 1;
            }


            // Sort names and print results
            Console.WriteLine("Sorting names....");
            var sortingService = serviceProvider.GetService<ISortingService>();
            string[] result = sortingService.SortByLastName(filePath, "sorted-names-list.txt");
            Console.WriteLine("Result:");
            foreach (string name in result)
            {
                Console.WriteLine(name);
            }

            // Return 0 if the code is successful. 
            // Return 1 if the the argument is invalid. 
            return 0;
        }
    }
}
