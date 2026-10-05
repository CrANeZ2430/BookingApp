using BookingApp.Application.Common;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.DomainEvents;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.DomainEventHandlers;

public class CreateBookingEventHandler(
    IEmailService emailService)
    : INotificationHandler<DomainNotification<CreateBookingEvent>>
{
    public async Task Handle(
        DomainNotification<CreateBookingEvent> notification, 
        CancellationToken cancellationToken = default)
    {
        await emailService.SendBookingCreationEmailAsync(
            notification.DomainEvent.BookingId,
            notification.DomainEvent.OccurredAt,
            notification.DomainEvent.Email,
            cancellationToken);
    }
}