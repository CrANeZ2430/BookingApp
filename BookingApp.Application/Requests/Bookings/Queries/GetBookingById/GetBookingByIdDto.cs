namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingById;

public record GetBookingByIdDto(
    Guid BookingId,
    int AttendeeCount,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    Guid MemberId,
    Guid RoomId);