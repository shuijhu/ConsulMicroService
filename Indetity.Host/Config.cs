using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace HeLian.Xiaoyi.Indetity.Host
{
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which IdentityResources will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resources = new List<ApiResource>();
            var items = Configuration.GetSection("APIResources").Get<IList<ResourceMo>>();
            foreach (var item in items)
            {
                resources.Add(new ApiResource(item.Name, item.Description));
            }
            return resources;
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>();
            var items = Configuration.GetSection("IdentityClients").Get<IList<ClientMo>>();
            foreach(var item in items)
            {
                clients.Add(new Client
                {
                    ClientId = item.ClientId,
                    ClientSecrets = new[] { new Secret(item.ClientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new[] { "ProjectService", "UserService" }
                });
            }
            return clients;

        }
    }
    public class ClientMo
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class ResourceMo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
