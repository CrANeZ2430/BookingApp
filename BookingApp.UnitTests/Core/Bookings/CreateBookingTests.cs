using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.DomainEvents;
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
    private readonly IDateTimeProvider _dateTimeProvider 
        = TestDataFactory.GetDateTimeProvider().Object;

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
            _memberId,
            _roomId,
            _dateTimeProvider,
            "random.email@gmail.com");

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
            _memberId,
            _roomId,
            _dateTimeProvider,
            "random.email@gmail.com");
        
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
            _memberId,
            _roomId,
            _dateTimeProvider,
            "random.email@gmail.com");
        
        //Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Room attendees count cannot be 0.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("hddjdhfdjfhd")]
    public void Create_Should_ThrowDomainException_WhenEmailIsInvalid(string email)
    {
        //Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
        
        //Act
        var act = () => Booking.Create(
            20,
            startTime,
            endTime,
            _memberId,
            _roomId,
            _dateTimeProvider,
            email);
        
        //Assert
        act.Should().Throw<DomainException>()
            .WithMessage("A valid email is required.");
    }

    [Fact]
    public void Create_Should_AddValidDomainEvent_WhenDataIsValid()
    {
        //Arrange
        var attendeeCount = 10;

        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);

        var email = "random.email@gmail.com";
        
        //Act
        var booking = Booking.Create(
            attendeeCount,
            startTime,
            endTime,
            _memberId,
            _roomId,
            _dateTimeProvider,
            email);
        
        //Assert
        var createEvent = booking.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeOfType<CreateBookingEvent>()
            .Subject;
        
        createEvent.BookingId.Should().Be(booking.BookingId);
        createEvent.Email.Should().Be(email);
        createEvent.OccurredAt.Should().Be(_dateTimeProvider.GetCurrentDateTime());
    }
}