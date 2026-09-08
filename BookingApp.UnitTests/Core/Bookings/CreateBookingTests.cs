using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Exceptions;
using BookingApp.UnitTests.Fakes;
using FluentAssertions;

namespace BookingApp.UnitTests.Core.Bookings;

public class CreateBookingTests
{
    private readonly Guid _memberId = Guid.NewGuid();
    private readonly Guid _roomId = Guid.NewGuid();
    private readonly DateTime _utcNow = TestDataFactory.GetUtcNow();

    [Fact]
    public void Create_Should_SetStatusToPending_WhenDataIsValid()
    {
        //Arrange
        var attendeeCount = 10;

        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);

        //Act
        var booking = Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            _utcNow,
            _memberId,
            _roomId);

        // Assert
        booking.Status.Should().Be(BookingStatus.Pending);
        booking.MemberId.Should().Be(_memberId);
        booking.RoomId.Should().Be(_roomId);
    }

    [Fact]
    public void Create_Should_ThrowDomainException_WhenStartTimeIsAfterEndTime()
    {
        //Arrange
        var attendeeCount = 10;
        
        var startTime = _utcNow.AddDays(2);
        var endTime = _utcNow.AddDays(1);
        
        //Act
        var act = () => Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            _utcNow,
            _memberId,
            _roomId);
        
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
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
        
        //Act
        var act = () => Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            _utcNow,
            _memberId,
            _roomId);
        
        //Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Room attendees count cannot be 0.");
    }
}