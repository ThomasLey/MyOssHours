using Microsoft.OpenApi.Models;

namespace MyOssHours.Backend.REST.StartUp;

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerBackend(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            //var scheme = new OpenApiSecurityScheme
            //{
            //    In = ParameterLocation.Header,
            //    Name = "Authorization",
            //    Flows = new OpenApiOAuthFlows
            //    {
            //        AuthorizationCode = new OpenApiOAuthFlow
            //        {
            //            AuthorizationUrl = new Uri("http://localhost:8080/realms/master/protocol/openid-connect/auth"),
            //            TokenUrl = new Uri("http://localhost:8080/realms/master/protocol/openid-connect/token"),
            //            Scopes =
            //            {
            //                { "myosshours.read", "Read access to MyOssHours" },
            //                { "myosshours.contribute", "Contribute access to MyOssHours" },
            //                { "openid", "OpenId" },
            //                { "profile", "Profile" },
            //                { "verification", "Verification" }
            //            }
            //        }
            //    },
            //    Type = SecuritySchemeType.OAuth2
            //};

            //options.AddSecurityDefinition("OIDC", scheme);

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference { Id = "OIDC", Type = ReferenceType.SecurityScheme }
            //        },
            //        new List<string>()
            //    }
            //});
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerBackend(this IApplicationBuilder app)
    {
        // Todo: Enable Swagger Configuration
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            //c.OAuthClientId("web");
            //c.OAuthScopes("profile", "verification", "openid", "myosshours.read", "myosshours.contribute");
            ////c.OAuthScopes("myosshours.contribute");
            //c.OAuthUsePkce();
            //c.EnablePersistAuthorization();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        });


        return app;
    }
}