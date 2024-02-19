using MyOssHours.Backend.Application;
using MyOssHours.Backend.Infrastructure;
using MyOssHours.Backend.Presentation;
using MyOssHours.Backend.REST.StartUp;

var builder = WebApplication.CreateBuilder(args);

// add clean architecture layers
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

builder.Services.AddAuthenticationAuthorization();
builder.Services.AddSwaggerBackend();

var app = builder.Build();

app.UseHttpsRedirection();

// add clean architecture layers
app
    .UseApplication()
    .UseInfrastructure()
    .UsePresentation();

app.UseAuthenticationAuthorization();
app.UseSwaggerBackend();

app.Run();