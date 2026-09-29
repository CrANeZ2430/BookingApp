using System.Net;
using System.Net.Http.Headers;
using BookingApp.IntegrationTests.Fakes;
using BookingApp.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookingApp.IntegrationTests.Features.Members;

[Collection("IntegrationTests")]
public class GetMembersEndpointTests(
    CustomWebApplicationFactory factory)
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetMembers_Should_ReturnOk_WhenHasReadMembersPermission()
    {
        var page = 0;
        var pageSize = 5;

        var token = TestJwtGenerator.GenerateToken("read:members");
        
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, 
                token);
        
        var response = await _client.GetAsync(
            $"/api/members?page={page}&pageSize={pageSize}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetMembers_Should_ReturnUnauthorized_WhenUnAuthorised()
    {
        var page = 0;
        var pageSize = 5;
        
        var response = await _client.GetAsync(
            $"/api/members?page={page}&pageSize={pageSize}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetMembers_Should_ReturnForbidden_WhenDoesNotHaveReadMembersPermission()
    {
        var page = 0;
        var pageSize = 5;

        var token = TestJwtGenerator.GenerateToken();
        
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, 
                token);
        
        var response = await _client.GetAsync(
            $"/api/members?page={page}&pageSize={pageSize}");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}