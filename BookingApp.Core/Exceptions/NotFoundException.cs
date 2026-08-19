namespace BookingApp.Core.Exceptions;

public class NotFoundException(string errorMessage) : BaseException(errorMessage);