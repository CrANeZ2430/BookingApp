using MediatR;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingById;

public record GetBookingByIdQuery(
    Guid BookingId)
    : IRequest<GetBookingByIdDto>;