using System.Diagnostics;
using EMS.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var kind = request is ICommandBase ? "Command" : "Query";
            logger.LogInformation("Handling {kind} {RequestName} {@Request}", kind, requestName, request);
            var sw = Stopwatch.StartNew();
            try
            {
                var response = await next(cancellationToken);
                logger.LogInformation("Handled {Kind} {RequestName} in {ElapsedMs}ms", kind, requestName, sw.ElapsedMilliseconds);
                return response;

            }
            catch (Exception ex)
            {
                sw.Stop();
                logger.LogError(ex, "Failed {Kind} {RequestName} after {ElapsedMs}ms",
                    kind, requestName, sw.ElapsedMilliseconds);
                throw;
            }
        }
    }
}