using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace MyOssIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "myosshours.read", displayName: "My OSS Hours Read Access"),
                new ApiScope(name: "myosshours.contribute", displayName: "My OSS Hours Contribute Access")
            };

    public static IEnumerable<Client> Clients =>
            new Client[]
                { new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "myosshours.read", "myosshours.contribute" }
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { "https://localhost:6003/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:6003/signout-callback-oidc" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "myosshours.read", "myosshours.contribute"
                }
            }
        };
}