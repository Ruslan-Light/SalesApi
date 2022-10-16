using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Исключение для 404
        /// </summary>
        /// <param name="name"> Наименование типа объекта </param>
        /// <param name="key"> Параметр объекта </param>
        /// <param name="detail"> Текст ошибки </param>
        public NotFoundException(string message = "Запрашиваемый объект не найден") : base(message)
        {
            Detail = message;
        }

        public NotFoundException(string message, string detail) : base(message)
        {
            Detail = detail;
        }

        public string Detail { get; }
    }
}
