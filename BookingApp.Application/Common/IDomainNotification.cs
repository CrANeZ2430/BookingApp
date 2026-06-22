using BookingApp.Core.Common;
using MediatR;

namespace BookingApp.Application.Common;

public interface IDomainNotification<out TEvent> : INotification
    where TEvent : IDomainEvent
{
    TEvent DomainEvent { get; }
}