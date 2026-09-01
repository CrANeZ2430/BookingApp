using BookingApp.Core.Exceptions;

namespace BookingApp.Application.Exceptions;

public class NotFoundException(string errorMessage) : BaseException(errorMessage);