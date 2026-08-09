using MediatR;

namespace BookingApp.Application.Requests.Bookings.Commands.UpdateBooking;

public record UpdateBookingCommand(
    Guid BookingId,
    UpdateBookingDto Dto)
    : IRequest;