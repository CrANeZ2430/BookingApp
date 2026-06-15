using BookingApp.Core.Abstractions;

namespace BookingApp.Infrastructure.Abstractions;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime() => DateTime.UtcNow;
}