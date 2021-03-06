using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using SGM.Manager.API.Extensions;
using SGM.Manager.Application.Commands;
using SGM.Manager.Application.Commands.Departamento;
using SGM.Manager.Application.Commands.Funcionario;
using SGM.Manager.Application.Commands.Integracoes;
using SGM.Manager.Application.Commands.Usuario;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using SGM.Shared.Core.Contracts.UnitOfWork;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Clinica", policy => policy.RequireClaim("Clinica", ETipoFuncionario.Clinica.ToString()));
                options.AddPolicy("Gestao", policy => policy.RequireClaim("Gestao", ETipoFuncionario.Gestao.ToString()));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(auth =>
            {
                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("4DFF9B8EBB5314B9A62EFA72DA8B4D7658231C05")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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
            services.AddScoped<ICommandHandler<CadastrarIntegracaoCommand>, IntegracaoHandler>();
            services.AddScoped<ICommandHandler<DeletarIntegracoesCommand>, IntegracaoHandler>();
            services.AddScoped<ICommandHandler<EditarIntegracoesCommand>, IntegracaoHandler>();
            services.AddScoped<ICommandHandler<CadastrarUsuarioCidadoCommand>, UsuarioHandler>();

            //Repos
            //serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IIntegracaoRepository, IntegracaoRepository>();
            services.AddScoped<ICidadaoUserRepository, CidadaoUserRepository>();
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
