using BookingApp.Application.Common;
using BookingApp.Core.Domain.Bookings.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public class GetBookingsByMemberIdQueryHandler(
    IBookingsRepository bookingsRepository)
    : IRequestHandler<GetBookingsByMemberIdQuery, PageResponse<GetBookingsByMemberIdDto>>
{
    public async Task<PageResponse<GetBookingsByMemberIdDto>> Handle(
        GetBookingsByMemberIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var (bookings, totalCount) = 
            await bookingsRepository
                .GetByMemberIdAsync(
                    request.Page,
                    request.PageSize,
                    request.MemberId,
                    cancellationToken);
        
        var bookingDtos = 
            bookings.Select(b => 
                new GetBookingsByMemberIdDto(
                    b.BookingId,
                    b.AttendeeCount,
                    b.StartTime,
                    b.EndTime,
                    b.CreatedAt,
                    b.MemberId,
                    b.RoomId)).ToArray();

        return new PageResponse<GetBookingsByMemberIdDto>(
            request.Page,
            request.PageSize,
            totalCount,
            bookingDtos);
    }
}