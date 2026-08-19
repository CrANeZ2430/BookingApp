namespace BookingApp.Core.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string errorMessage, IDictionary<string, string[]> errors) : base(errorMessage)
    {
        Errors = errors;
    }
    
    public BadRequestException(string errorMessage) : base(errorMessage)
    {
        
    }
    
    public IDictionary<string, string[]> Errors { get; set; }
}