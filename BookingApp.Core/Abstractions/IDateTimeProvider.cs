namespace BookingApp.Core.Abstractions;

public interface IDateTimeProvider
{
    DateTime GetCurrentDateTime();
}