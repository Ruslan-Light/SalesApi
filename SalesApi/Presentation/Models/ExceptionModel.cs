using Newtonsoft.Json;

namespace Presentation.Models
{
    /// <summary>
    /// Модель, возвращаемая при возникновении исключения
    /// </summary>
    public class ExceptionModel
    {
        public ExceptionModel(string errorMessage, string detail)
        {
            ErrorMessage = errorMessage;
            Detail = detail;
        }

        public ExceptionModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Detail = "При выполнении операции произошла ошибка";
        }

        /// <summary>
        /// Текст ошибки из исключения
        /// </summary>
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Описание ошибки для отображения на клиенте
        /// </summary>
        [JsonProperty("Detail")]
        public string Detail { get; set; }
    }
}
