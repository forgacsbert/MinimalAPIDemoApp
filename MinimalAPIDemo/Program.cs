using MinimalAPIDemo.EndpointDefinitions.Extensions;

// Program class used as a starting point of the application.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();

app.UseEndpointDefinition();
app.UseHttpsRedirection();
app.Run();

