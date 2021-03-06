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
        public DbSet<Atendimentos> Atendimentos { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Medicos> Medicos { get; set; }

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckDatabaseStatus() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Paciente>().OwnsOne(p => p.CPF);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
