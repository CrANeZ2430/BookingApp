using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Infrastructure.Database;
using BookingApp.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.IntegrationTests.Features.PublishDomainEventInterceptor;

[Collection("IntegrationTests")]
public class PublishDomainEventInterceptorTests(
    CustomWebApplicationFactory factory)
{
    [Fact]
    public async Task SaveChangesAsync_Should_ClearDomainEvents_And_PublishViaMediator()
    {
        //Arrange
        using var scope = factory.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider
            .GetRequiredService<BookingAppDbContext>();
        var dateTimeProvider = scope.ServiceProvider
            .GetRequiredService<IDateTimeProvider>();
            
        var member = Member.Create(
            "random-auth0-id",
            "alex",
            "alex",
            Roles.Customer,
            "random@gmail.com",
            "+48575176838",
            dateTimeProvider);

        //Pre-Assert
        member.DomainEvents.Should().NotBeEmpty();
            
        //Act
        await dbContext.Members.AddAsync(member);
        await dbContext.SaveChangesAsync();
            
        //Assert
        member.DomainEvents.Should().BeEmpty();
    }
}