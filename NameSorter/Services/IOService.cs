﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace NameSorter
{
    public class IOService : IIOService
    {
        private readonly ILogger<IOService> _logger;

        public IOService(ILogger<IOService> logger)
        {
            _logger = logger;
        }

        public string[] ReadFromTextFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public void WriteToTextFile(string fileName, string[] content)
        {
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            try
            {
                File.WriteAllLines(savePath, content);
                _logger.LogInformation("Saved to location {fileLocation}", savePath);
            }
            catch(Exception e)
            {
                _logger.LogError("Failed to save file to {fileLocation} with exception " + e, savePath);
            }
                    
        }
    }
}
