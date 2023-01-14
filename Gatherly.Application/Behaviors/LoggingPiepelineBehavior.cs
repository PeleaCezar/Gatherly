using Gatherly.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Gatherly.Application.Behaviors
{
    public class LoggingPiepelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingPiepelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPiepelineBehavior(
            ILogger<LoggingPiepelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        //We can change default logging level configuration from appsettings.Development with desired default logging configuration
        //Ex : If default is Warning, then Information logs will not be logger
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            var result = await next();

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error,
                    DateTime.UtcNow);
            }

            _logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return result;
        }
    }
}
