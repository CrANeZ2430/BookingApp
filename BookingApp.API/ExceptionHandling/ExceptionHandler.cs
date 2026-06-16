using Microsoft.AspNetCore.Diagnostics;

namespace BookingApp.API.ExceptionHandling;

public class ExceptionHandler(
    IProblemDetailsService problemDetailService,
    ILogger<ExceptionHandler> logger, 
    IExceptionMapper mapper) 
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = mapper.MapException(exception);
        
        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        try
        {
            await problemDetailService.WriteAsync(new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = problemDetails
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to write problem details response.");
        }

        return true;
    }
}