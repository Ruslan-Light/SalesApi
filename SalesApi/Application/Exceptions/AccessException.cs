using System;

namespace Application.Exceptions
{
    public class AccessException : Exception
    {
        public AccessException(string message = "Доступ запрещен.") : base(message)
        {
            Detail = message;
        }

        public AccessException(string message, string detail) : base(message)
        {
            Detail = detail;
        }

        public string Detail { get; }
    }
}
