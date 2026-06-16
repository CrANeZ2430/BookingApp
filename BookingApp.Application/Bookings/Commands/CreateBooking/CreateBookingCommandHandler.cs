using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Core.Domain.Rooms.Repositories;
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
            throw new ArgumentException("Given member was not found.");
        if (room is null)
            throw new ArgumentException("Given room was not found.");
        if (request.AttendeeCount > room.Capacity)
            throw new ArgumentOutOfRangeException(
                $"{nameof(room.Capacity)}, {nameof(request.AttendeeCount)}", 
                $"The room capacity is {room.Capacity}, but you requested {request.AttendeeCount} attendees.");
        if (await bookingsRepository.HasOverlappingAsync(request.RoomId, request.StartTime, request.EndTime,
                cancellationToken))
            throw new ArgumentOutOfRangeException(
                $"{nameof(request.StartTime)}, {nameof(request.EndTime)}", 
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