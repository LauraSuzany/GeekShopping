﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "admin";
        public const string Customer = "customer";

        public static IEnumerable<IdentityResource> IdentityResource => 
            new List<IdentityResource> 
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("geek_shopping", "GeekShopping Server"),
                new ApiScope(name: "read", "Read Data."),
                new ApiScope(name: "write", "Write Data."),
                new ApiScope(name: "delete", "delete Data."),
            };
        public static IEnumerable<Client> apiScopes =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("my_super_secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, 
                    AllowedScopes = {"read", "write", "prfile"},
                },
                new Client
                {
                    ClientId = "geek_shopping",
                    ClientSecrets = { new Secret("my_super_secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code, 
                    RedirectUris = {"http://localhost:1386/signin-oidc" },
                    PostLogoutRedirectUris = {"http://localhost:1386/signout-callback-oidc" }, 
                    AllowedScopes = new List<string> 
                    {
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "geek_shopping"
                    }
                }
            };
    }
}
