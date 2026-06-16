namespace BookingApp.API.ExceptionHandling.Exceptions;

public class UnauthorizedException(string message) : Exception(message);