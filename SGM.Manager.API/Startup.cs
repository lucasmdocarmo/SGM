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
using SGM.Manager.API.Extensions;
using SGM.Manager.Application.Commands;
using SGM.Manager.Application.Commands.Departamento;
using SGM.Manager.Application.Commands.Funcionario;
using SGM.Manager.Application.Commands.Usuario;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
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
            services.AddSwaggerConfig();

            services.AddDbContext<ManagerContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppConnString")));
            services.AddScoped<ManagerContext>();
            services.AddScoped<IUnitOfWork, ManagerContext>();

            //Application
            services.AddScoped<ICommandResult, CommandResult>();
            services.AddScoped<ICommandHandler<CadastrarDepartamentoCommand>, DepartamentoHandler>();
            services.AddScoped<ICommandHandler<DeletarDepartamentoCommand>, DepartamentoHandler>();
            services.AddScoped<ICommandHandler<EditarDepartamentoCommand>, DepartamentoHandler>();
            services.AddScoped<ICommandHandler<CadastrarUsuarioCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<DeletarUsuarioCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<EditarUsuarioCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<CadastrarFuncionarioCommand>, FuncionarioHandler>();
            services.AddScoped<ICommandHandler<DeletarFuncionarioCommand>, FuncionarioHandler>();
            services.AddScoped<ICommandHandler<EditarFuncionarioCommand>, FuncionarioHandler>();



            //Repos
            //serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.EnsureMigrationOfContext<ManagerContext>();
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
