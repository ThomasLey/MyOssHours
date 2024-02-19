using MyOssHours.Backend.Application;
using MyOssHours.Backend.Infrastructure;
using MyOssHours.Backend.Presentation;
using MyOssHours.Backend.REST.StartUp;

namespace MyOssHours.Backend.REST;

public static class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // add clean architecture layers
        builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration)
            .AddPresentation(builder.Configuration);

        // add web layers
        builder.Services
            .AddAuthenticationAuthorization()
            .AddSwaggerBackend();

        var app = builder.Build();

        // add clean architecture layers
        app
            .UseApplication()
            .UseInfrastructure()
            .UsePresentation();

        // add web layers
        app.UseAuthenticationAuthorization();
        app.UseSwaggerBackend();

        app.UseHttpsRedirection();

        app.Run();

    }
}