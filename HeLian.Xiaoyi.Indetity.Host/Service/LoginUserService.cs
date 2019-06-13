using Consul;
using HeLian.Xiaoyi.Helper;
using HeLian.Xiaoyi.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HeLian.Xiaoyi.Indetity.Host.Service
{
    public class LoginUserService : ILoginUserService
    {
        private HttpClient _httpClient;
        private ConsulHelper _consulHelper;
        public LoginUserService(HttpClient httpClient,ConsulHelper consulHelper)
        {
            _httpClient = httpClient;
            _consulHelper = consulHelper;
        }
        public async Task<UserMo> ValidateUser(string userName, string password)
        {
            var uri = $"{_consulHelper.GetUri("UserService")}/native/user/{userName}";
            try
            {
                var res = await _httpClient.GetStringAsync(uri);

                return JsonConvert.DeserializeObject<UserMo>(res);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
