using BookingApp.Application.Common;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.DomainEvents;
using MediatR;

namespace BookingApp.Application.Requests.Members.DomainEventHandlers;

public class CreateMemberEventHandler(
    IEmailService emailService) 
    : INotificationHandler<DomainNotification<CreateMemberEvent>>
{
    public async Task Handle(
        DomainNotification<CreateMemberEvent> notification, 
        CancellationToken cancellationToken = default)
    {
        await emailService.SendMemberCreationEmailAsync(
            notification.DomainEvent.MemberId,
            notification.DomainEvent.OccurredAt,
            notification.DomainEvent.Email,
            cancellationToken);
    }
}