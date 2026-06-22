using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace BookingApp.API.Extensions;

public sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider) 
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document, 
        OpenApiDocumentTransformerContext context, 
        CancellationToken cancellationToken = default)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            
            var securityScheme = new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Header,
                BearerFormat = "JWT",
                Description = "Enter your JWT token directly into the input box below."
            };

            document.Components.SecuritySchemes["Bearer"] = securityScheme;

            var requirement = new OpenApiSecurityRequirement()
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
            };
            
            document.Security ??= new List<OpenApiSecurityRequirement>();
            document.Security.Add(requirement);
        }
    }
}