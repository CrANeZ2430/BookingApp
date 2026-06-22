namespace BookingApp.Core.Common;

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}