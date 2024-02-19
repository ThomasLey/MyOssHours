using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Infrastructure.Repositories;

namespace MyOssHours.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyOssHoursDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();

        return services;
    }

    // use infrastructure with db initialization
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MyOssHoursDbContext>();
        db.Database.EnsureCreated();
        scope.Dispose();

        return app;
    }
}

// create project repository and implement IProjectsRepository