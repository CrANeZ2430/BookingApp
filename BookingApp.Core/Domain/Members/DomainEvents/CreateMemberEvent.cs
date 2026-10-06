using BookingApp.Core.Common;

namespace BookingApp.Core.Domain.Members.DomainEvents;

public record CreateMemberEvent(
    DateTime OccurredAt, 
    string Email,
    Guid MemberId)
    : IDomainEvent;