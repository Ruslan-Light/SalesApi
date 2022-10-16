using Application.Extensions;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Application.Services
{
    public class LoggerService : ILoggerService
    {
        public void AddLoggerProvider(ILoggerFactory loggerFactory)
        {
            var path = Path.Join(Environment.CurrentDirectory, $"\\Logs\\{DateTime.Now.Year}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, $"{DateTime.Now.Month}_logs.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            loggerFactory.AddFile(filePath);
        }
    }
}
