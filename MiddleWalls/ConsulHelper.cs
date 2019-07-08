using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeLian.Xiaoyi.Helper
{
    public class ConsulHelper
    {
        private ConsulClient _consulClient;
        public ConsulHelper(ConsulClient client)
        {
            _consulClient = client;
        }
        public string GetUri(string serviceName)
        {
                var services = _consulClient.Agent.Services().Result.Response.Values.Where(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
                if (services.Any())
                {
                    var service = services.ElementAt(Environment.TickCount % services.Count());
                    return $"http://{service.Address}:{service.Port}";
                }
            return "";
        }
    }
}
