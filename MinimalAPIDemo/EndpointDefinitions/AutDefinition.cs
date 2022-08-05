using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalAPIDemo.EndpointDefinitions.Extensions;

namespace MinimalAPIDemo.EndpointDefinitions;

public class AutDefinition : IEndpointDefinition
{
    public bool ShouldRunLast => true;

    public void DefineEndpoints(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });
    }
}
