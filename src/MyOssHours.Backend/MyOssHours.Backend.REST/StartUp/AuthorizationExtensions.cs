namespace MyOssHours.Backend.REST.StartUp;

internal static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthenticationAuthorization(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters.ValidateAudience = false;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("MyOssHoursScope.Read", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "myosshours.read");
                policy.RequireClaim("scope", "verification");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "openid");
            });
        });

        return services;
    }

    // UseAuthorization
    public static IApplicationBuilder UseAuthenticationAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthorization();

        return app;
    }
}