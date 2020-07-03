// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            {
                new ApiResource("api1", "My API1"),
                new ApiResource("api2", "My API2"),
                new ApiResource("api3", "My API3")
                {
                    Scopes =
                    {
                        new Scope("api3.get1", "Get1 scope of Resource API3"),
                        new Scope("api3.get2", "Get2 scope of Resource API3"),
                        new Scope("api3.default", "Default scope of Resource API3"),
                    }
                }
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "fahad1",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenType = AccessTokenType.Reference,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(GetClientSecret())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" },
                },
                new Client
                {
                    ClientId = "fahad2",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(GetClientSecret())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api2" },
                },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                },
                // JavaScript Client 1
                new Client
                {
                    ClientId = "js-client-1",
                    ClientName = "JavaScript Client 1",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris =           { "http://localhost:4201/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4201/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:4201" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1",
                        "api3.get1",
                        "api3.default"
                    }
                },
                // JavaScript Client 2
                new Client
                {
                    ClientId = "js-client-2",
                    ClientName = "JavaScript Client 2",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris =           { "http://localhost:4202/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4202/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:4202" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api2",
                        "api3.get2"
                    }
                },
                // JavaScript Client 3
                new Client
                {
                    ClientId = "js-client-3",
                    ClientName = "JavaScript Client 3",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris =           { "http://localhost:4203/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4203/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:4203" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1",
                        "api2",
                        "api3.get1",
                        "api3.get2",
                        "api3.default"
                    }
                }
            };

        public static string GetClientSecret()
        {
            var secret = "4drwpqZnWkNSABp8PUNA".Sha256();
            return secret;
        }

        public static string GetClientUserName() => TestUsers.Users.FirstOrDefault(user => user.SubjectId == "88421114").Username;
        public static string GetClientPassword() => TestUsers.Users.FirstOrDefault(user => user.SubjectId == "88421114").Password;
    }
}