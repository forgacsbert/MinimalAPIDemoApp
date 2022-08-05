namespace MinimalAPIDemo.EndpointDefinitions.Extensions;

public interface IEndpointDefinition
{
    /// <summary>
    /// Method used to define the endpoints for the application.
    /// </summary>
    /// <param name="app">Represents the application which needs to be configured.</param>
    void DefineEndpoints(WebApplication app);

    /// <summary>
    /// Method used to define the services for the application.
    /// </summary>
    /// <param name="services">Represents the service collection to which we will subscribe our services.</param>
    void DefineServices(IServiceCollection services);
}