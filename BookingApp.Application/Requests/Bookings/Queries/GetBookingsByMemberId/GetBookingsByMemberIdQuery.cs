using BookingApp.Application.Common;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public record GetBookingsByMemberIdQuery(
    Guid MemberId,
    int Page,
    int PageSize)
    : IRequest<PageResponse<GetBookingsByMemberIdDto>>;