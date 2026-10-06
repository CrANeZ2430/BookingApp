using BookingApp.Core.Common;

namespace BookingApp.Core.Domain.Bookings.DomainEvents;

public record CreateBookingEvent(
    DateTime OccurredAt, 
    string Email,
    Guid BookingId) 
    : IDomainEvent;