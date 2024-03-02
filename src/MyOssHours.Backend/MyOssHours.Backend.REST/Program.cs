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

        // add REST http/s layers
        builder.Services
            .AddAuthenticationAuthorization()
            .AddSwaggerBackend();

        var app = builder.Build();

        // add clean architecture layers. Be careful about the order of these calls.
        // The order is important to preserve the execution order of the middleware.
        app
            .UseApplication()
            .UseInfrastructure()
            .UsePresentation();

        // add REST http/s layers
        app.UseSwaggerBackend();
        app.UseRouting(); // this is required to use endpoints

        app.UseAuthenticationAuthorization();
        app.UseLoginLogoutUrls();

        app.UseHttpsRedirection();

        app.Run();
    }
}