using MediatR;

namespace BookingApp.Application.Requests.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    int AttendeeCount,
    DateTime StartTime,
    DateTime EndTime,
    Guid MemberId,
    Guid RoomId)
    : IRequest<Guid>;