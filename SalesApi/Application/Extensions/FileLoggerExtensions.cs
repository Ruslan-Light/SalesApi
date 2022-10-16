using Application.Tools.Logger;
using Microsoft.Extensions.Logging;
using System.IO;
using System;

namespace Application.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory)
        {
            var pathToYear = Path.Combine(Directory.GetCurrentDirectory(), "Logs", DateTime.UtcNow.Year.ToString());

            if (!Directory.Exists(pathToYear))
            {
                Directory.CreateDirectory(pathToYear);
            }

            string filePath = Path.Combine(pathToYear, $"{DateTime.UtcNow.Month}_logger.log");

            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
