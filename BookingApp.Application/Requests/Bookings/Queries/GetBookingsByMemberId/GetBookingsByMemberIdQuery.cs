using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public record GetBookingsByMemberIdQuery(
    Guid MemberId,
    int Page,
    int PageSize)
    : IRequest<IReadOnlyCollection<GetBookingsByMemberIdDto>>;