using MediatR;

namespace BookingApp.Application.Requests.Bookings.Commands.DeleteBooking;

public record DeleteBookingCommand(
    Guid BookingId)
    : IRequest;