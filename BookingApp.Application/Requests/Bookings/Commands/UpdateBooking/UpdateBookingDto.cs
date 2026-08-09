namespace BookingApp.Application.Requests.Bookings.Commands.UpdateBooking;

public record UpdateBookingDto(
    int AttendeeCount,
    DateTime StartTime,
    DateTime EndTime,
    Guid RoomId);