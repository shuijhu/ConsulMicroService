using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using HeLian.Xiaoyi.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HeLian.Xiaoyi.UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var consulClient = new ConsulClient(c => c.Address = new Uri($"http://{ Configuration["Consul:IP"] }:{Configuration["Consul:Port"] }"));
            ConsulHelper helper = new ConsulHelper(consulClient);
            services.AddAuthentication(Configuration["Identity:Scheme"])
                .AddIdentityServerAuthentication(ops =>
                {
                    ops.RequireHttpsMetadata = false;
                    //ops.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
                    ops.Authority = helper.GetUri("IdentityService");
                    ops.ApiName = Configuration["Service:Name"];
                });

            services.AddSingleton(consulClient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();

            app.RegisterConsul(lifetime, new ServiceEntity()
            {
                IP = Configuration["Service:IP"],
                Port = Convert.ToInt32(Configuration["Service:Port"]),
                ServiceName = Configuration["Service:Name"],
                ConsulIP = Configuration["Consul:IP"],
                ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
            });
        }
    }
}
