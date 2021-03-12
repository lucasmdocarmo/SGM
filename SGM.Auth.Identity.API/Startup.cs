using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SGM.Auth.Identity.API.Auth.Resources;
using SGM.Auth.Identity.API.Auth.Users;
using SGM.Auth.Identity.API.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using SGM.Auth.Identity.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SGM.Auth.Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static string AuthAssembly => typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AuthContext>(builder => builder.UseSqlServer(Configuration.GetConnectionString("AppConnString"), 
                                              sqlOptions => sqlOptions.MigrationsAssembly(AuthAssembly)));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthContext>();

            //services.AddIdentityServer().AddInMemoryClients(AuthClients.Get())
            //                            .AddInMemoryIdentityResources(AuthResources.GetIdentityResources())
            //                            .AddInMemoryApiResources(AuthResources.GetApiResources())
            //                            .AddInMemoryApiScopes(AuthResources.GetApiScopes())
            //                            .AddDeveloperSigningCredential();

            var ids = services.AddIdentityServer().AddDeveloperSigningCredential();

            ids.AddOperationalStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("AppConnString"), sqlOptions => sqlOptions.MigrationsAssembly(AuthAssembly)))
                .AddConfigurationStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("AppConnString"), sqlOptions => sqlOptions.MigrationsAssembly(AuthAssembly)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SGM.Auth.Identity.API", Version = "v1" });
            });

            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGM.Auth.Identity.API v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
