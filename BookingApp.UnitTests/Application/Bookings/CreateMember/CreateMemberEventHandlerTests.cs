using BookingApp.Application.Common;
using BookingApp.Application.Requests.Members.DomainEventHandlers;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.DomainEvents;
using BookingApp.UnitTests.Fakes;
using Moq;

namespace BookingApp.UnitTests.Application.Bookings.CreateMember;

public class CreateMemberEventHandlerTests
{
    private readonly DateTime _utcNow = TestDataFactory.GetUtcNow();
    
    [Fact]
    public async Task Handle_Should_SendEmail_WhenEventIsFired()
    {
        //Arrange
        var emailService = new Mock<IEmailService>();
        var domainNotification = new DomainNotification<CreateMemberEvent>(
            new CreateMemberEvent(_utcNow, "random@gmail.com", Guid.NewGuid()));
        var handler = new CreateMemberEventHandler(emailService.Object);

        //Act
        await handler.Handle(domainNotification, It.IsAny<CancellationToken>());
        
        // Assert
        emailService.Verify(
            x => x.SendMemberCreationEmailAsync(
                domainNotification.DomainEvent.MemberId,
                domainNotification.DomainEvent.OccurredAt,
                domainNotification.DomainEvent.Email,
                It.IsAny<CancellationToken>()), 
            Times.Once);
    }
}