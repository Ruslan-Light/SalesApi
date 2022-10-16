using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();

            _logger.LogInformation($"Выполняется запрос {typeof(TRequest).Name}");

            TResponse response;

            try
            {
                stopWatch.Start();

                _logger.LogInformation("Тело запрос: {@request}", request);

                response = await next();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.InnerException);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogInformation($"Запрос {typeof(TRequest).Name} выполнен за {stopWatch.ElapsedMilliseconds}мс");
            }

            return response;
        }
    }
}
