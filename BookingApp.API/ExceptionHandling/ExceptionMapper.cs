using BookingApp.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.ExceptionHandling;

public class ExceptionMapper : IExceptionMapper
{
    public ProblemDetails MapException(Exception exception)
    {
        int statusCode;
        string title;
        string type;
        var errors = new Dictionary<string, string[]>();

        switch (exception)
        {
            case ValidationException e:
                statusCode = 400;
                title = "Validation failed.";
                type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1";
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
                type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1";
                break;

            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                title = "Resource cannot be found";
                type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5";
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                title = "Internal server problem";
                type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1";
                break;
        }

        var problemDetails = new ProblemDetails()
        {
            Type = type,
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