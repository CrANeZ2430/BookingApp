using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingApp.IntegrationTests.Fakes;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string DefaultScheme = "TestScheme";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("No Authorization header provided."));
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "auth0|test-user-id-123"),
            new Claim(ClaimTypes.Email, "testuser@bookingapp.com"),
            /*new Claim("permissions", "bookings:create"),
            new Claim("permissions", "bookings:read")*/
        };

        var identity = new ClaimsIdentity(claims, DefaultScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, DefaultScheme);
        
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}