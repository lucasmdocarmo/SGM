using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SGM.Gestao.Domain.Entities;
using SGM.Gestao.Domain.Entities.Usuarios;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Gestao.Infra.Context
{
    public class GestaoContext: DbContext, IUnitOfWork
    {
        public GestaoContext(DbContextOptions<GestaoContext> options) : base(options) { }

        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Instituicoes> Instituicoes { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Informacoes> Informacoes { get; set; }
        public DbSet<Projetos> Projetos { get; set; }

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckDatabaseStatus() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Funcionarios>().OwnsOne(p => p.CPF);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
