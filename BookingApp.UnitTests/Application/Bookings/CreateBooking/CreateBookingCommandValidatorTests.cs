using BookingApp.Application.Requests.Bookings.Commands.CreateBooking;
using BookingApp.Core.Abstractions;
using BookingApp.UnitTests.Common;
using FluentAssertions;
using Moq;

namespace BookingApp.UnitTests.Application.Bookings.CreateBooking;

public class CreateBookingCommandValidatorTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProvider = 
        TestDataFactory.GetDateTimeProvider();
    
    [Fact]
    public async Task Validate_Should_BeValid_WhenDataIsValid()
    {
        // Arrange
        var startTime = _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1);
        var endTime = _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2);
        
        var command = new CreateBookingCommand(
            5,
            startTime,
            endTime,
            Guid.NewGuid(),
            Guid.NewGuid());
        
        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);
    
        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task Validate_Should_HaveValidationError_WhenAttendeeCountIsBelowOne(int attendeeCount)
    {
        // Arrange
        var command = new CreateBookingCommand(
            attendeeCount,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1),
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);
    
        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);
    
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(
            e => e.PropertyName == nameof(command.AttendeeCount));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task Validate_Should_HaveValidationError_WhenStartTimeIsInPast(int daysOffset)
    {
        // Arrange

        var command = new CreateBookingCommand(
            5,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(daysOffset),
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(
            e => e.PropertyName == nameof(command.StartTime));
    }
    
    [Fact]
    public async Task Validate_Should_HaveValidationError_WhenStartTimeIsEmpty()
    {
        // Arrange
        var command = new CreateBookingCommand(
            5,
            default,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.StartTime));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task Validate_Should_HaveValidationError_WhenEndTimeIsInPast(int daysOffset)
    {
        // Arrange

        var command = new CreateBookingCommand(
            5,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(daysOffset),
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.EndTime));
    }
    
    [Fact]
    public async Task Validate_Should_HaveValidationError_WhenEndTimeIsEmpty()
    {
        // Arrange
        var command = new CreateBookingCommand(
            5,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1),
            default,
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.EndTime));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Validate_Should_HaveValidationError_WhenEndTimeIsBeforeOrEqualStartTime(int daysOffset)
    {
        // Arrange
        var startTime = _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1);
        
        var command = new CreateBookingCommand(
            5,
            startTime,
            startTime.AddDays(daysOffset),
            Guid.NewGuid(),
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);
    
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.EndTime));
    }

    [Fact]
    public async Task Validate_Should_HaveValidationError_WhenMemberIdIsEmpty()
    {
        // Arrange
        var command = new CreateBookingCommand(
            5,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1),
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            Guid.Empty,
            Guid.NewGuid());

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);
    
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.MemberId));
    }
    
    [Fact]
    public async Task Validate_Should_HaveValidationError_WhenRoomIdIsEmpty()
    {
        // Arrange
        var command = new CreateBookingCommand(
            5,
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(1),
            _dateTimeProvider.Object.GetCurrentDateTime().AddDays(2),
            Guid.NewGuid(),
            Guid.Empty);

        var validator = new CreateBookingCommandValidator(_dateTimeProvider.Object);

        // Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);
    
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(
            e => e.PropertyName == nameof(command.RoomId));
    }
}