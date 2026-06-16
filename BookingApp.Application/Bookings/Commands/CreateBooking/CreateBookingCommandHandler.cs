using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IMembersRepository membersRepository,
    IRoomsRepository roomsRepository,
    IBookingsRepository bookingsRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateBookingCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateBookingCommand request, 
        CancellationToken cancellationToken = default)
    {
        var room = await roomsRepository.GetByIdAsync(request.RoomId, cancellationToken);
        if ((await membersRepository.GetByIdAsync(request.MemberId, cancellationToken)) is null)
            throw new NotFoundException("Given member was not found.");
        if (room is null)
            throw new NotFoundException("Given room was not found.");
        if (!room.IsOperational)
            throw new BadRequestException("Given room is under renovation.");
        if (request.AttendeeCount > room.Capacity)
            throw new BadRequestException(
                $"The room capacity is {room.Capacity}, but you requested {request.AttendeeCount} attendees.");
        if (await bookingsRepository.HasOverlappingAsync(request.RoomId, request.StartTime, request.EndTime,
                cancellationToken))
            throw new BadRequestException(
                "Booking time isn't available");

        var booking = Booking.Create(
            request.AttendeeCount,
            request.StartTime,
            request.EndTime,
            dateTimeProvider.GetCurrentDateTime(),
            request.MemberId,
            request.RoomId);

        await bookingsRepository.AddAsync(booking, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.BookingId;
    }
}