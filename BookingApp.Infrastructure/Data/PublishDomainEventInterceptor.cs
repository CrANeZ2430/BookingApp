using BookingApp.Application.Common;
using BookingApp.Core.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookingApp.Infrastructure.Data;

public class PublishDomainEventInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var aggregates = eventData.Context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();
        
        aggregates.ForEach(x => x.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            var wrapperType = typeof(DomainNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = (INotification)Activator.CreateInstance(wrapperType, domainEvent)!;
            
            await publisher.Publish(notification, cancellationToken);
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}