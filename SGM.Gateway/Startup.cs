using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Gateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //var authenticationProviderKey = "IdentityApiKey";
            //services.AddAuthentication().AddJwtBearer(identity =>
            //{
            //     identity.Authority = "https://localhost:5006";
            //     identity.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateAudience = false
            //     };

            // });

            services.AddOcelot();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => 
                { 
                    await context.Response.WriteAsync("API Gateway is Running."); 
                });
            });

            app.UseOcelot().Wait();
        }
    }
}
