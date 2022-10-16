using Microsoft.Extensions.Logging;

namespace Application.Interfaces
{
    public interface ILoggerService
    {
        public void AddLoggerProvider(ILoggerFactory loggerFactory);
    }
}
