namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public record GetBookingsByMemberIdDto(
    Guid BookingId,
    int AttendeeCount,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    Guid MemberId,
    Guid RoomId);