using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SGM.Saude.Domain.Entities;
using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Domain.Entities.Consulta;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Saude.Infra.Context
{
    public sealed class SaudeContext :DbContext, IUnitOfWork
    {
        public SaudeContext(DbContextOptions<SaudeContext> options) : base(options) { }

        public DbSet<Consultas> Consultas { get; set; } 
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Prescricao> Prescricao { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Medicos> Medicos { get; set; }

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckIfDatabaseExists() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        
    }
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var paciente1 = new Paciente("Lucas Mariano", DateTime.Now, "11640810633", "13944854", true,
                Shared.Core.ValueObjects.ETipoStatusPaciente.Aguardando, "paciente teste", "dermatologia");

            var clinica1 = new Clinica("clinica Contagem", "33939393");
            var medico1 = new Medicos("Dr. Joao", "Medico", "234234234", "dermatologista", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "", "",
                    300M, true, clinica1.Id);

            var consulta1 = new Consultas("dermatologia", "Consuta", "", Convert.ToDateTime("03-27-2021"), true, paciente1.Id, medico1.Id);

            modelBuilder.Entity<Paciente>().HasData(paciente1);
            modelBuilder.Entity<Clinica>().HasData(clinica1);
            modelBuilder.Entity<Medicos>().HasData(medico1);
            modelBuilder.Entity<Consultas>().HasData(consulta1);

        }
    }
}
