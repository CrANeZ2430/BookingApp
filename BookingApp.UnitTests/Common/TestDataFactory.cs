using BookingApp.Core.Abstractions;
using Moq;

namespace BookingApp.UnitTests.Common;

public static class TestDataFactory
{
    public static Mock<IDateTimeProvider> GetDateTimeProvider()
    {
        var utcNow = new DateTime(
            2026, 
            1, 
            1, 
            12, 
            0, 
            0, 
            DateTimeKind.Utc);
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentDateTime())
            .Returns(utcNow);

        return dateTimeProvider;
    }
    
    public static DateTime GetUtcNow()
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