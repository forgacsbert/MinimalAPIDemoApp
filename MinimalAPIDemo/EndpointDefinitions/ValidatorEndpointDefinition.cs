using FluentValidation.AspNetCore;
using MinimalAPIDemo.EndpointDefinitions.Extensions;

namespace MinimalAPIDemo.EndpointDefinitions;

public class ValidatorEndpointDefinition : IEndpointDefinition
{
    public bool ShouldRunLast => false;

    public void DefineEndpoints(WebApplication app)
    {
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IEndpointDefinition>());
    }
}
