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
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            var dbContext = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();

            var room = await dbContext.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            var member = Member.Create(
                "auth0|test-id-123",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923",
                dateTimeProvider);

            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            
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
        
        var token = TestJwtGenerator.GenerateToken();
            
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, 
                token);

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
            var dbContext = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();
            
            var member = Member.Create(
                "auth0|test-id-124",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923",
                dateTimeProvider);

            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            
            memberId = member.MemberId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            memberId, 
            Guid.NewGuid());
        
        var token = TestJwtGenerator.GenerateToken();
            
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, 
                token);

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
            var dbContext = scope.ServiceProvider.GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();

            var room = await dbContext.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            roomId = room.RoomId;
            now = dateTimeProvider.GetCurrentDateTime();
        }
        
        var command = new CreateBookingCommand(
            5,
            now.AddDays(1),
            now.AddDays(2),
            Guid.NewGuid(), 
            roomId);
        
        var token = TestJwtGenerator.GenerateToken();
            
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, 
                token);

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
            var dbContext = scope.ServiceProvider
                .GetRequiredService<BookingAppDbContext>();
            var dateTimeProvider = scope.ServiceProvider
                .GetRequiredService<IDateTimeProvider>();

            var room = await dbContext.Rooms
                .FirstAsync(x => x.Name == "Turing Room 101");
            
            var member = Member.Create(
                "auth0|test-id-125",
                "John",
                "Doe",
                Roles.Customer,
                "j.doe@gmail.com",
                "+48374465923",
                dateTimeProvider);

            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            
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