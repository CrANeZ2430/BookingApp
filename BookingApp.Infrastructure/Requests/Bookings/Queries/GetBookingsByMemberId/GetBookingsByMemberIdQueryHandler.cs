using BookingApp.Application.Common;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Bookings.Queries.GetBookingsByMemberId;

public class GetBookingsByMemberIdQueryHandler(
    BookingAppDbContext dbContext)
    : IRequestHandler<GetBookingsByMemberIdQuery, PageResponse<GetBookingsByMemberIdDto>>
{
    public async Task<PageResponse<GetBookingsByMemberIdDto>> Handle(
        GetBookingsByMemberIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Bookings.AsNoTracking()
            .Where(x => x.MemberId == request.MemberId);

        var totalCount = await query.CountAsync(cancellationToken);
        
        var bookings = await query
            .OrderBy(x => x.StartTime)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetBookingsByMemberIdDto(
                x.BookingId,
                x.AttendeeCount,
                x.StartTime,
                x.EndTime,
                x.CreatedAt,
                x.MemberId,
                x.RoomId))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<GetBookingsByMemberIdDto>(
            request.Page,
            request.PageSize,
            totalCount,
            bookings);
    }
}