using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookingApp.IntegrationTests.Fakes;

public static class TestJwtGenerator
{
    public static readonly SymmetricSecurityKey SecurityKey 
        = new(Encoding.UTF8.GetBytes("SuperSecretTestKeyThatIsAtLeast32BytesLong!"));

    public static string GenerateToken(params string[] permissions)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "auth0|test-user-id-123"),
            new(ClaimTypes.Email, "testuser@bookingapp.com")
        };

        foreach (var perm in permissions)
        {
            claims.Add(new Claim("permissions", perm));
        }

        var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: new DateTime(
                2026, 
                1, 
                1, 
                12, 
                0, 
                0, 
                DateTimeKind.Utc).AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}