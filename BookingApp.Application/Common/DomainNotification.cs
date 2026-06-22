using BookingApp.Core.Common;
using MediatR;

namespace BookingApp.Application.Common;

public class DomainNotification<TEvent>(TEvent domainEvent) : IDomainNotification<TEvent>
    where TEvent : IDomainEvent
{
    public TEvent DomainEvent { get; } = domainEvent;
}