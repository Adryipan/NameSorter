using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NameSorter
{
    class Program
    {
        static int Main(string[] args)
        {
            // Check if one and only one argument is provided. Exit the program if not.
            if(args.Length == 0)
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

            //Setup the host for the app
            var host = CreateHostBuilder().Build();
            

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    // Get the input service
                    var inputService = services.GetRequiredService<IInputService>();
                    Console.WriteLine($"Reading file {filePath}");
                    string[] nameFile = inputService.ReadFromTextFile(filePath);


                    // Get Sorting service
                    var sortingService = services.GetRequiredService<ISortingService>();
                    Console.WriteLine("Sorting names....");
                    string[] result = sortingService.SortByLastName(nameFile);
                    Console.WriteLine("Result:");
                    foreach (string name in result)
                    {
                        Console.WriteLine(name);
                    }
                }
                catch(Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured.");
                }


            }


            // Return 0 if the code is successful. 
            // Return 1 if the the argument is invalid. 
            return 0;
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Inject depedency
                services.AddTransient<IInputService, InputService>();
                services.AddTransient<ISortingService, SortingService>();
            });

    }
}
