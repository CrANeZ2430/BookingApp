using BookingApp.Core.Domain.Members.DomainEvents;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.UnitTests.Fakes;
using FluentAssertions;

namespace BookingApp.UnitTests.Core.Members;

public class CreateMemberTests
{
    [Fact]
    public void Create_Should_AddValidDomainEvent_WhenDataIsValid()
    {
        //Arrange

        var dateTimeProvider = TestDataFactory.GetDateTimeProvider().Object;
        
        //Act
        var member = Member.Create(
            "random-auth0-id",
            "alex",
            "alex",
            Roles.Customer,
            "random@gmail.com",
            "+48237123564",
            dateTimeProvider);
        
        //Assert
        var createEvent = member.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeOfType<CreateMemberEvent>()
            .Subject;

        createEvent.MemberId.Should().Be(member.MemberId);
        createEvent.Email.Should().Be(member.Email);
        createEvent.OccurredAt.Should().Be(dateTimeProvider.GetCurrentDateTime());
    }
}