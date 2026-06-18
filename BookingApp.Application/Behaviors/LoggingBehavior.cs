using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingApp.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken = default)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Starting request {RequestName}", requestName);

        var timer = Stopwatch.StartNew();

        try
        {
            var response = await next(cancellationToken);
            return response;
        }
        finally
        {
            timer.Stop();
            var elapsedMilliseconds = timer.ElapsedMilliseconds;
            
            logger.LogInformation("Finished request {RequestName} in {ElapsedMilliseconds}ms", 
                requestName, elapsedMilliseconds);

            if (elapsedMilliseconds > 500)
            {
                logger.LogWarning("Long Running Request Detected: {RequestName} took {ElapsedMilliseconds}ms", 
                    requestName, elapsedMilliseconds);
            }
        }
    }
}