using BookingApp.Application.Common;
using BookingApp.Application.Requests.Bookings.DomainEventHandlers;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.DomainEvents;
using BookingApp.UnitTests.Fakes;
using Moq;

namespace BookingApp.UnitTests.Application.Bookings.CreateBooking;

public class CreateBookingEventHandlerTests
{
    private readonly DateTime _utcNow = TestDataFactory.GetUtcNow();
    
    [Fact]
    public async Task Handle_Should_SendEmail_WhenEventIsFired()
    {
        //Arrange
        var emailService = new Mock<IEmailService>();
        var domainNotification = new DomainNotification<CreateBookingEvent>(
            new CreateBookingEvent(_utcNow, "random@gmail.com", Guid.NewGuid()));
        var handler = new CreateBookingEventHandler(emailService.Object);

        //Act
        await handler.Handle(domainNotification, It.IsAny<CancellationToken>());
        
        // Assert
        emailService.Verify(
            x => x.SendBookingCreationEmailAsync(
                domainNotification.DomainEvent.BookingId,
                domainNotification.DomainEvent.OccurredAt,
                domainNotification.DomainEvent.Email,
                It.IsAny<CancellationToken>()), 
            Times.Once);
    }
}