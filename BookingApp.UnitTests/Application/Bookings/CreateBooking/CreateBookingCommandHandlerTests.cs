using BookingApp.Application.Exceptions;
using BookingApp.Application.Requests.Bookings.Commands.CreateBooking;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Core.Domain.Rooms.Models;
using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace BookingApp.UnitTests.Application.Bookings.CreateBooking;

public class CreateBookingCommandHandlerTests
{
    private readonly Mock<IRoomsRepository> _roomsRepoMock = new Mock<IRoomsRepository>();
    private readonly Mock<IMembersRepository> _membersRepoMock = new Mock<IMembersRepository>();
    private readonly Mock<IBookingsRepository> _bookingsRepoMock = new Mock<IBookingsRepository>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IDateTimeProvider> _dateTimeProvider = TestDataFactory.GetDateTimeProvider();
    private readonly DateTime _utcNow = TestDataFactory.GetUtcNow();
    private readonly CreateBookingCommandHandler _handler;

    public CreateBookingCommandHandlerTests()
    {
        _handler = new CreateBookingCommandHandler(
            _membersRepoMock.Object, 
            _roomsRepoMock.Object, 
            _bookingsRepoMock.Object, 
            _unitOfWorkMock.Object, 
            _dateTimeProvider.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnBookingId_WhenDataIsValid()
    {
        // Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
    
        var room = Room.Create(
            "Auditory 103.", 
            3, 
            30, 
            Equipment.Projector,
            true, 
            Guid.NewGuid());

        var member = Member.Create(
            "example",
            "Alex",
            "Alex",
            Roles.Customer,
            "a.alex@gmail.com",
            "+48123456789");

        _roomsRepoMock
            .Setup(x => x.GetByIdAsync(room.RoomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        _membersRepoMock
            .Setup(x => x.GetByIdAsync(member.MemberId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var command = new CreateBookingCommand(
            4,
            startTime,
            endTime,
            member.MemberId,
            room.RoomId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        
        _bookingsRepoMock.Verify(
            b => b.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), 
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenMemberDoesNotExist()
    {
        // Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
        
        var room = Room.Create(
            "Auditory 103.", 
            3, 
            30, 
            Equipment.Projector,
            true, 
            Guid.NewGuid());

        _roomsRepoMock
            .Setup(x => x.GetByIdAsync(room.RoomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);
        
        var command = new CreateBookingCommand(
            4,
            startTime,
            endTime,
            Guid.NewGuid(),
            room.RoomId);
        
        //Act
        var act = () => 
            _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Given member was not found.");
    }
    
    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
        
        var member = Member.Create(
            "example",
            "Alex",
            "Alex",
            Roles.Customer,
            "a.alex@gmail.com",
            "+48123456789");

        _membersRepoMock
            .Setup(x => x.GetByIdAsync(member.MemberId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);
        
        var command = new CreateBookingCommand(
            4,
            startTime,
            endTime,
            member.MemberId,
            Guid.NewGuid());
        
        //Act
        var act = () => 
            _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Given room was not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnBadRequest_WhenAttendeeCountExceedsRoomCapacity()
    {
        //Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
    
        var room = Room.Create(
            "Auditory 103.", 
            3, 
            30, 
            Equipment.Projector,
            true, 
            Guid.NewGuid());

        var member = Member.Create(
            "example",
            "Alex",
            "Alex",
            Roles.Customer,
            "a.alex@gmail.com",
            "+48123456789");

        _roomsRepoMock
            .Setup(x => x.GetByIdAsync(
                room.RoomId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        _membersRepoMock
            .Setup(x => x.GetByIdAsync(
                member.MemberId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var command = new CreateBookingCommand(
            40,
            startTime,
            endTime,
            member.MemberId,
            room.RoomId);
        
        //Act
        var act = () => 
            _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .Where(x => 
                ((IEnumerable<string>)x.Errors[nameof(command.AttendeeCount)]).SequenceEqual(
                new[] { $"The room capacity is {room.Capacity}, " +
                        $"but you requested {command.AttendeeCount} attendees." }));
    }

    [Fact]
    public async Task Handle_Should_ThrowBadRequest_WhenRoomIsNotOperational()
    {
        //Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
    
        var room = Room.Create(
            "Auditory 103.", 
            3, 
            30, 
            Equipment.Projector,
            false, 
            Guid.NewGuid());

        var member = Member.Create(
            "example",
            "Alex",
            "Alex",
            Roles.Customer,
            "a.alex@gmail.com",
            "+48123456789");

        _roomsRepoMock
            .Setup(x => x.GetByIdAsync(
                room.RoomId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        _membersRepoMock
            .Setup(x => x.GetByIdAsync(
                member.MemberId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var command = new CreateBookingCommand(
            4,
            startTime,
            endTime,
            member.MemberId,
            room.RoomId);
        
        //Act
        var act = () =>
            _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .Where(x => 
                ((IEnumerable<string>)x.Errors[nameof(command.RoomId)])
                .SequenceEqual(new[] { "Given room is under renovation." }));
    }
    
    [Fact]
    public async Task Handle_Should_ThrowBadRequest_WhenBookingTimeOverlaps()
    {
        // Arrange
        var startTime = _utcNow.AddDays(1);
        var endTime = _utcNow.AddDays(2);
    
        var room = Room.Create(
            "Auditory 103.", 
            3, 
            30, 
            Equipment.Projector,
            true, 
            Guid.NewGuid());

        var member = Member.Create(
            "example",
            "Alex",
            "Alex",
            Roles.Customer,
            "a.alex@gmail.com",
            "+48123456789");

        _roomsRepoMock
            .Setup(x => x.GetByIdAsync(
                room.RoomId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        _membersRepoMock
            .Setup(x => x.GetByIdAsync(
                member.MemberId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        _bookingsRepoMock
            .Setup(x => x.HasOverlappingAsync(
                room.RoomId, 
                startTime, 
                endTime))
            .ReturnsAsync(true);

        var command = new CreateBookingCommand(
            4,
            startTime,
            endTime,
            member.MemberId,
            room.RoomId);

        // Act
        var act = () => 
            _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .Where(x => 
                        ((IEnumerable<string>)x.Errors[nameof(command.StartTime)])
                        .SequenceEqual(new[] { "Booking time isn't available." }) &&
                        ((IEnumerable<string>)x.Errors[nameof(command.EndTime)])
                        .SequenceEqual(new[] { "Booking time isn't available." }));
    }
}