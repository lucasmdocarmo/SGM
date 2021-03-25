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
using SGM.Cidadao.API.Extensions;
using SGM.Cidadao.Application;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Application.Commands.Cidadao;
using SGM.Cidadao.Application.Commands.Contribuinte;
using SGM.Cidadao.Application.Commands.Endereco;
using SGM.Cidadao.Application.Commands.Impostos;
using SGM.Cidadao.Application.Commands.StatusContribuinte;
using SGM.Cidadao.Application.Queries;
using SGM.Cidadao.Application.Queries.Cidadao;
using SGM.Cidadao.Application.Queries.Results.Cidadao;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using SGM.Shared.Core.Contracts.UnitOfWork;
using SGM.Shared.Core.Queries;
using SGM.Shared.Core.Queries.Handler;
using SGM.Shared.Core.ValueObjects;
using System.Text;

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

            }).AddJsonOptions(json => { json.JsonSerializerOptions.IgnoreNullValues = true; }).SetCompatibilityVersion(CompatibilityVersion.Latest);


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
            //Identiy
            //services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer", options =>
            //{
            //    options.ApiName = "cidadao";
            //    options.Authority = "https://localhost:5006";
            //});

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            // }).AddIdentityServerAuthentication(options =>
            // {
            //    options.ApiName = "cidadao";
            //    options.ApiSecret = "cidadao-secret";
            //    options.Authority = "https://localhost:5006";
            //    options.RequireHttpsMetadata = false;
            //});

            services.AddCors();
            services.AddControllers();
            services.AddSwaggerConfig();

            services.AddDbContext<CidadaoContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppConnString")));
            services.AddScoped<CidadaoContext>();
            services.AddScoped<IUnitOfWork, CidadaoContext>();

            //Application
            services.AddScoped<ICommandResult, CommandResult>();
            services.AddScoped<ICommandHandler<CadastrarCidadaoCommand>, CidadaoHandler>();
            services.AddScoped<ICommandHandler<DeletarCidadaoCommand>, CidadaoHandler>();
            services.AddScoped<ICommandHandler<EditarCidadaoCommand>, CidadaoHandler>();
            services.AddScoped<ICommandHandler<CadastrarContribuicaoCommand>, ContribuinteHandler>();
            services.AddScoped<ICommandHandler<DeletarContribuicaoCommand>, ContribuinteHandler>();
            services.AddScoped<ICommandHandler<EditarContribuicaoCommand>, ContribuinteHandler>();
            services.AddScoped<ICommandHandler<CadastrarImpostosCommand>, ImpostosHandler>();
            services.AddScoped<ICommandHandler<DeletarImpostosCommand>, ImpostosHandler>();
            services.AddScoped<ICommandHandler<EditarImpostosCommand>, ImpostosHandler>();
            services.AddScoped<ICommandHandler<CadastrarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<DeletarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<EditarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<CadastrarStatusCommand>, StatusContribuinteHandler>();
            services.AddScoped<ICommandHandler<DeletarStatusCommand>, StatusContribuinteHandler>();
            services.AddScoped<ICommandHandler<EditarStatusCommand>, StatusContribuinteHandler>();

            //Queries
            services.AddScoped<IQueryResult, QueryResult>();
            services.AddScoped<IQueryHandler<CidadaoQueryResult>, CidadaoQueries>();
            services.AddScoped<IQueryHandler<ConsultarConsultaMedicaQuery>, CidadaoQueries>();

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
