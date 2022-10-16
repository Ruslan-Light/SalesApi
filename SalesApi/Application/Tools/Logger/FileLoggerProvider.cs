using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;

namespace Application.Tools.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _path;
        private Component component = new Component();
        private bool disposed = false;

        public FileLoggerProvider(string path)
        {
            _path = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_path);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                component.Dispose();
            }

            disposed = true;
        }

        ~FileLoggerProvider()
        {
            Dispose(false);
        }
    }
}
