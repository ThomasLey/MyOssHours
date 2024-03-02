using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyOssHours.Backend.REST.StartUp;

internal static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthenticationAuthorization(this IServiceCollection services)
    {
        // Add services to the container.
        //services.AddAuthentication()
        //    .AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.Authority = "http://localhost:8080";
        //        options.TokenValidationParameters.ValidateAudience = false;
        //    });

        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("MyOssHoursScope.Read", policy =>
        //    {
        //        policy.RequireAuthenticatedUser();
        //        policy.RequireClaim("scope", "myosshours.read");
        //        policy.RequireClaim("scope", "verification");
        //        policy.RequireClaim("scope", "profile");
        //        policy.RequireClaim("scope", "openid");
        //    });
        //});

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.ConsentCookie.IsEssential = true;

            options.CheckConsentNeeded = context => false;

        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/login");
                options.LogoutPath = new PathString("/logout");
            });


        return services;
    }

    // UseAuthorization
    public static IApplicationBuilder UseAuthenticationAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication().UseCookiePolicy();
        app.UseAuthorization();

        return app;
    }

    // use login logout urls
    public static IApplicationBuilder UseLoginLogoutUrls(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/login", (string sid, string email, string name, HttpContext context) =>
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, sid),
                    new(ClaimTypes.Email,email),
                    new(ClaimTypes.Name, name),
                    new(ClaimTypes.Role, "myosshours.contribute"),
                    new(ClaimTypes.Role, "myosshours.read"),};

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                var hc = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>().HttpContext
                    ?? throw new ArgumentNullException(nameof(HttpContext), "http context is null. wired");
                hc.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait();

                return Task.CompletedTask;
            });

            endpoints.MapGet("/logout", context =>
            {
                var hc = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>().HttpContext
                         ?? throw new ArgumentNullException(nameof(HttpContext), "http context is null. wired");
                hc.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                context.Response.Redirect("/");
                return Task.CompletedTask;
            });
        });

        return app;
    }
}