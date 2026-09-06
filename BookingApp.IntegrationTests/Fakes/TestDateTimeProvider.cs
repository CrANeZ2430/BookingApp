using BookingApp.Core.Abstractions;

namespace BookingApp.IntegrationTests.Fakes;

public class TestDateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime()
    {
        return new DateTime(
            2026, 
            1, 
            1, 
            12, 
            0, 
            0, 
            DateTimeKind.Utc);
    }
}