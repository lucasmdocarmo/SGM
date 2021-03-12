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
using SGM.Cidadao.API.Extensions;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Contracts.UnitOfWork;

namespace SGM.Cidadao.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
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

            }).AddJsonOptions(json => { json.JsonSerializerOptions.IgnoreNullValues = true; })
              .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddCors();
            services.AddControllers();
            services.AddSwaggerConfig();

            services.AddDbContext<CidadaoContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppConnString")));
            services.AddScoped<CidadaoContext>();
            services.AddScoped<IUnitOfWork, CidadaoContext>();

            //Application
            services.AddScoped<ICommandResult, CommandResult>();

            //Repos
            //serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICidadaoRepository, CidadaoRepository>();
            services.AddScoped<IContribuicaoRepository, ContribuicaoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IImpostosRepository, ImpostosRepository>();
            services.AddScoped<IStatusContribuicaoRepository, StatusContribuicaoRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.EnsureMigrationOfContext<CidadaoContext>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "SGM.Cidadao"); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
