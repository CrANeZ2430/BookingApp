using BookingApp.Application.Exceptions;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingById;
using BookingApp.Core.Exceptions;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Bookings.Queries.GetBookingById;

public class GetBookingByIdQueryHandler(
    BookingAppDbContext dbContext) 
    : IRequestHandler<GetBookingByIdQuery, GetBookingByIdDto>
{
    public async Task<GetBookingByIdDto> Handle(
        GetBookingByIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var booking = await dbContext.Bookings.AsNoTracking()
            .FirstOrDefaultAsync(x => x.BookingId == 
                                      request.BookingId, cancellationToken);

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