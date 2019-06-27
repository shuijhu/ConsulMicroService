using HeLian.Xiaoyi.Indetity.Host.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.Indetity.Host.Controller
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class LoginController: ControllerBase
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        public LoginController(HttpClient httpClient, IConfiguration configuration) {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Web([FromBody]LoginMo model)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = _configuration["IdentityClients:0:Name"];
            dict["client_secret"] = _configuration["IdentityClients:0:ClientSecret"];
            dict["grant_type"] = _configuration["IdentityClients:0:GrantType"];
            dict["username"] = model.UserName;
            dict["password"] = model.Password;

            using (var content = new FormUrlEncodedContent(dict))
            {
                var uri = $"http://localhost:5000{_configuration["IdentityService:TokenUri"]}";
                var msg = await _httpClient.PostAsync(uri, content);
                if (!msg.IsSuccessStatusCode)
                {
                    return StatusCode(Convert.ToInt32(msg.StatusCode));
                }

                string result = await msg.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
        }
    }
}
