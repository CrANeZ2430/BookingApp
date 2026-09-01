namespace BookingApp.Core.Exceptions;

public class DomainException(string errorMessage) : BaseException(errorMessage);