using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.IdentityService
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResource()
        {
            var res = new List<ApiResource>();
            res.Add(new ApiResource() { Name = "UserAPI", Description = "用户API" });
            res.Add(new ApiResource() { Name = "ProjectAPI", Description = "项目API" });

            return res;
        }

        public static IEnumerable<Client> GetClients()
        {
            var res = new List<Client>();
            res.Add(new Client()
            {
                ClientId = "webapp",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("123987".Sha256()) { } },
                AllowedScopes = { "UserAPI", "ProjectAPI" },
            });

            return res;
        }
    }
}
