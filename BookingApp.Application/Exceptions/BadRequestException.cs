using BookingApp.Core.Exceptions;

namespace BookingApp.Application.Exceptions;

public class BadRequestException(string errorMessage, IDictionary<string, string[]> errors) : BaseException(errorMessage)
{
    public IDictionary<string, string[]> Errors { get; private set; } = errors;
}