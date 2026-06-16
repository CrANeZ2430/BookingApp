using BookingApp.API.ExceptionHandling.Exceptions;
using BookingApp.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.ExceptionHandling;

public class ExceptionMapper : IExceptionMapper
{
    public ProblemDetails MapException(Exception exception)
    {
        int statusCode;
        string title;
        var errors = new Dictionary<string, string[]>();

        switch (exception)
        {
            case ValidationException e:
                statusCode = 400;
                title = "Validation failed.";
                errors = e.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorCode)
                            .ToArray());
                break;
            case BadRequestException:
                statusCode = StatusCodes.Status400BadRequest;
                title = "Error caused by request";
                break;

            case UnauthorizedException:
                statusCode = StatusCodes.Status401Unauthorized;
                title = "Unauthorized access";
                break;

            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                title = "Resource cannot be found";
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                title = "Internal server problem";
                break;
        }

        var problemDetails = new ProblemDetails()
        {
            Type = exception.GetType().Name,
            Status = statusCode,
            Title = title
        };

        if (errors.Count == 0)
        {
            problemDetails.Detail = exception.Message;
        }
        else
        {
            problemDetails.Extensions.Add("errors", errors);
        }

        return problemDetails;
    }
}