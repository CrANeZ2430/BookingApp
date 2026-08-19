namespace BookingApp.Core.Exceptions;

public class BaseException(string errorMessage) : Exception(errorMessage);