using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Exceptions;
using BookingApp.UnitTests.Common;
using FluentAssertions;

namespace BookingApp.UnitTests.Core.Bookings;

public class BookingsTests
{
    private Guid MemberId { get; set; } = Guid.NewGuid();
    private Guid RoomId { get; set; } = Guid.NewGuid();
    private DateTime UtcNow { get; set; } = TestDataFactory.GetUtcNow();

    [Fact]
    public void Create_Should_SetStatusToPending_WhenDataIsValid()
    {
        //Arrange
        var attendeeCount = 10;

        var startTime = UtcNow.AddDays(1);
        var endTime = UtcNow.AddDays(2);

        //Act
        var booking = Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            UtcNow,
            MemberId,
            RoomId);

        // Assert
        booking.Status.Should().Be(BookingStatus.Pending);
        booking.MemberId.Should().Be(MemberId);
        booking.RoomId.Should().Be(RoomId);
    }

    [Fact]
    public void Create_Should_ThrowDomainException_WhenStartTimeIsAfterEndTime()
    {
        //Arrange
        var attendeeCount = 10;
        
        var startTime = UtcNow.AddDays(2);
        var endTime = UtcNow.AddDays(1);
        
        //Act
        var act = () => Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            UtcNow,
            MemberId,
            RoomId);
        
        //Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Start time must be before end time.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Create_Should_ThrowDomainException_WhenAttendeeCountIsInvalid(int attendeeCount)
    {
        //Arrange
        var startTime = UtcNow.AddDays(1);
        var endTime = UtcNow.AddDays(2);
        
        //Act
        var act = () => Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            UtcNow,
            MemberId,
            RoomId);
        
        //Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Room attendees count cannot be 0.");
    }
}