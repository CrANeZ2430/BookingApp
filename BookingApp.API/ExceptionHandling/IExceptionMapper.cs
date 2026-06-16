using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.ExceptionHandling;

public interface IExceptionMapper
{
    ProblemDetails MapException(Exception exception);
}