using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BookingApp.Application.Requests.Bookings.Commands.CreateBooking;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Infrastructure.Database;
using BookingApp.IntegrationTests.Fakes;
using BookingApp.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.IntegrationTests.Features.Bookings;

[Collection("IntegrationTests")]
public class CreateBookingEndpointTests(
    CustomWebApplicationFactory factory)
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateBooking_Should_ReturnCreated_WhenPayloadIsValid()
    {
        // Arrange
        Guid roomId;
        Guid memberId;
        DateTime now;
        
        using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();

            var room = await context.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            var member = Member.Create(
                "auth0|test-id-123",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923");

            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
            
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(
                    TestAuthHandler.DefaultScheme, 
                    "default-ticket");
            
            roomId = room.RoomId;
            memberId = member.MemberId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            memberId, 
            roomId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/bookings", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateBooking_Should_ReturnNotFound_WhenRoomIdIsInvalid()
    {
        // Arrange
        Guid memberId;
        DateTime now;
        
        using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();
            
            var member = Member.Create(
                "auth0|test-id-124",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923");

            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
            
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(
                    TestAuthHandler.DefaultScheme, 
                    "default-ticket");
            
            memberId = member.MemberId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            memberId, 
            Guid.NewGuid());

        // Act
        var response = await _client.PostAsJsonAsync("/api/bookings", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateBooking_Should_ReturnNotFound_WhenMemberIdIsInvalid()
    {
        // Arrange
        Guid roomId;
        DateTime now;
        
        using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();

            var room = await context.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(
                    TestAuthHandler.DefaultScheme, 
                    "default-ticket");
            
            roomId = room.RoomId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            Guid.NewGuid(), 
            roomId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/bookings", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateBooking_Should_ReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        Guid roomId;
        Guid memberId;
        DateTime now;
        
        using (var scope = factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();

            var room = await context.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            var member = Member.Create(
                "auth0|test-id-125",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923");

            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
            
            roomId = room.RoomId;
            memberId = member.MemberId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            memberId, 
            roomId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/bookings", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}