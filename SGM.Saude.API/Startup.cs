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
using SGM.Saude.API.Extensions;
using SGM.Saude.Application.Commands;
using SGM.Saude.Application.Commands.Clinicas;
using SGM.Saude.Application.Commands.Pacientes;
using SGM.Saude.Application.Commands.Prescricao;
using SGM.Saude.Infra.Context;
using SGM.Saude.Infra.Repositories;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Saude.API
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

            services.AddDbContext<SaudeContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppConnString")));
            services.AddScoped<SaudeContext>();
            services.AddScoped<IUnitOfWork, SaudeContext>();

            //Application
            services.AddScoped<ICommandResult, CommandResult>();
            services.AddScoped<ICommandHandler<CadastrarClinicaCommand>, ClinicaHandler>();
            services.AddScoped<ICommandHandler<DeletarClinicaCommand>, ClinicaHandler>();
            services.AddScoped<ICommandHandler<EditarClinicaCommand>, ClinicaHandler>();
            services.AddScoped<ICommandHandler<CadastrarConsultaCommand>, ConsultaHandler>();
            services.AddScoped<ICommandHandler<DeletarConsultaCommand>, ConsultaHandler>();
            services.AddScoped<ICommandHandler<EditarConsultaCommand>, ConsultaHandler>();
            services.AddScoped<ICommandHandler<CadastrarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<DeletarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<EditarEnderecoCommand>, EnderecoHandler>();
            services.AddScoped<ICommandHandler<CadastrarMedicoCommand>, MedicoHandler>();
            services.AddScoped<ICommandHandler<DeletarMedicoCommand>, MedicoHandler>();
            services.AddScoped<ICommandHandler<EditarMedicoCommand>, MedicoHandler>();
            services.AddScoped<ICommandHandler<CadastrarPacienteCommand>, PacienteHandler>();
            services.AddScoped<ICommandHandler<DeletarPacienteCommand>, PacienteHandler>();
            services.AddScoped<ICommandHandler<EditarPacienteCommand>, PacienteHandler>();
            services.AddScoped<ICommandHandler<CadastrarPrescricaoCommand>, PrescricaoHandler>();
            services.AddScoped<ICommandHandler<DeletarPrescricaoCommand>, PrescricaoHandler>();
            services.AddScoped<ICommandHandler<EditarPrescricaoCommand>, PrescricaoHandler>();

            //Repos
            services.AddScoped<IClinicaRepository, ClinicaRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IMedicosRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPrescricaoRepository, PrescricaoRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.EnsureMigrationOfContext<SaudeContext>();
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
