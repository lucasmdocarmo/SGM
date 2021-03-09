using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGM.Manager.Infra.Context;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Manager.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public IUnitOfWork _unitOfWork { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));

            }).AddJsonOptions(json => { json.JsonSerializerOptions.IgnoreNullValues = true; }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddCors();
            services.AddControllers();

            services.AddDbContext<ManagerContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppConnString")));
            services.AddScoped<ManagerContext>();
            services.AddScoped<IUnitOfWork, ManagerContext>();
            _unitOfWork = services.BuildServiceProvider().GetRequiredService<IUnitOfWork>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ManagerContext appContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!_unitOfWork.CheckDatabaseStatus())
            {
                appContext.Database.EnsureCreated();
            }
            else { appContext.Database.Migrate(); }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
