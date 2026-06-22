using BookingApp.Application.Common;
using BookingApp.Core.Domain.Members.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingApp.Application.Requests.Members.DomainEventHandlers;

public class CreateMemberEventHandler(
    ILogger<CreateMemberEventHandler> logger) 
    : INotificationHandler<DomainNotification<CreateMemberEvent>>
{
    public Task Handle(
        DomainNotification<CreateMemberEvent> notification, 
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Member with id: {MemberId} was just created at {OccurredAt}", 
            notification.DomainEvent.MemberId, 
            notification.DomainEvent.OccurredAt);
        
        return Task.CompletedTask;
    }
}