using Duende.IdentityServer.Models;

namespace MyOssIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

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
            }};
}