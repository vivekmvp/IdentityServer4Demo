using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo3
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                // Add a resource for some set of APIs that we may be protecting
                // Note that the constructor will automatically create an allowed scope with
                // name and claims equal to the resource's name and claims. If the resource
                // has different scopes/levels of access, the scopes property can be set to
                // list specific scopes included in this resource, instead.
                new ApiResource(
                    "myAPIs",                                       // Api resource name
                    "My API Set #1")                                // Display name
                    //new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "office" }) // Claims to be included in access token
            };
        }

        //public static List<Scope> GetScopes()
        //{
        //    return new List<Scope>
        //    {
        //        new Scope
        //        {
        //            Name = "myApi",
        //            Description = "myApi description"
        //        }
        //    };
        //}

        //public static IEnumerable<Client> GetClients()
        //{
        //    return new List<Client>
        //    {
        //        new Client
        //        {
        //            ClientId = "resClient",
        //            ClientName = "Rest Client - Postman",
        //            //AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

        //            //RequireConsent = false,

        //            ClientSecrets = new List<Secret>
        //            {
        //                new Secret("topsecret".Sha256())
        //            },

        //            //RedirectUris = { "http://localhost:5002/signin-oidc" },
        //            //PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },


        //            AllowedScopes = new List<string>
        //            {
        //                "myApi"
        //            },
        //            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        //            AllowOfflineAccess = true 
        //        }
        //    };
        //}



    }

    public class CustomClientStore : IClientStore
    {
        public static IEnumerable<Client> AllClients { get; } = new[]
        {            
        new Client
        {
            ClientId = "myClient",
            ClientName = "My Custom Client",
            AccessTokenLifetime = 3600,
            AccessTokenType = AccessTokenType.Jwt,
            AllowOfflineAccess = true,            
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            RequireClientSecret = true,
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes =
            {
                "myAPIs"
            }
        }
    };

       


        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(AllClients.FirstOrDefault(c => c.ClientId == clientId));
        }
    }
}
