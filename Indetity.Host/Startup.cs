using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using HeLian.Xiaoyi.Helper;
using HeLian.Xiaoyi.Indetity.Host.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeLian.Xiaoyi.Indetity.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            InMemoryConfiguration.Configuration = this.Configuration;
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryClients(InMemoryConfiguration.GetClients())
            .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            //.AddProfileService<ProfileService>();

            //
            services.AddTransient<ILoginUserService, LoginUserService>();
            services.AddTransient<ConsulHelper>();
            services.AddSingleton(new ConsulClient(c => c.Address = new Uri($"http://{ Configuration["Consul:IP"] }:{Configuration["Consul:Port"] }")));
            services.AddSingleton<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

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
