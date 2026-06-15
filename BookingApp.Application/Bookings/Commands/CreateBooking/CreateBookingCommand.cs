using BookingApp.Core.Domain.Bookings.Models;
using MediatR;

namespace BookingApp.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    int AttendeeCount,
    DateTime StartTime,
    DateTime EndTime,
    Guid MemberId,
    Guid RoomId)
    : IRequest<Guid>;