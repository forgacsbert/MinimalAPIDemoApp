using FluentValidation.AspNetCore;
using MinimalAPIDemo.EndpointDefinitions.Extensions;

// Program class used as a starting point of the application.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();

app.UseEndpointDefinitions();
app.UseHttpsRedirection();
app.Run();

