using BookingApp.Core.Domain.Bookings.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public class GetBookingsByMemberIdQueryHandler(
    IBookingsRepository bookingsRepository)
    : IRequestHandler<GetBookingsByMemberIdQuery, IReadOnlyCollection<GetBookingsByMemberIdDto>>
{
    public async Task<IReadOnlyCollection<GetBookingsByMemberIdDto>> Handle(
        GetBookingsByMemberIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var bookings = await bookingsRepository
            .GetByMemberIdAsync(
                request.Page,
                request.PageSize,
                request.MemberId,
                cancellationToken);

        return bookings.Select(b => new GetBookingsByMemberIdDto(
            b.BookingId,
            b.AttendeeCount,
            b.StartTime,
            b.EndTime,
            b.CreatedAt,
            b.MemberId,
            b.RoomId)).ToArray();
    }
}