using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingApp.IntegrationTests.Fakes;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("No Authorization header"));
        }
        
        var token = authHeader.ToString().Replace(
            $"{JwtBearerDefaults.AuthenticationScheme} ", 
            "", StringComparison.OrdinalIgnoreCase).Trim();
        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid token format"));
        }

        var jwtToken = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(
            jwtToken.Claims, 
            JwtBearerDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(
            principal, 
            JwtBearerDefaults.AuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}