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
        if ((await membersRepository.GetByIdAsync(request.MemberId, cancellationToken)) is null)
            throw new ArgumentException("Given member was not found.");
        if ((await roomsRepository.GetByIdAsync(request.RoomId, cancellationToken)) is null)
            throw new ArgumentException("Given room was not found.");
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