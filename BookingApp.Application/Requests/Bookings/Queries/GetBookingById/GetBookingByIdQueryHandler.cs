using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingById;

public class GetBookingByIdQueryHandler(
    IBookingsRepository bookingsRepository) 
    : IRequestHandler<GetBookingByIdQuery, GetBookingByIdDto>
{
    public async Task<GetBookingByIdDto> Handle(
        GetBookingByIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var booking = await bookingsRepository.GetByIdAsync(
            request.BookingId, 
            cancellationToken);

        if (booking is null)
            throw new NotFoundException("Given booking was not found.");

        return new GetBookingByIdDto(
            booking.BookingId,
            booking.AttendeeCount,
            booking.StartTime,
            booking.EndTime,
            booking.CreatedAt,
            booking.MemberId,
            booking.RoomId);
    }
}