using Microsoft.EntityFrameworkCore;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SGM.Cidadao.Domain.Entities;
using Flunt.Notifications;
using System.Linq;

namespace SGM.Cidadao.Infra.Context
{
    public class CidadaoContext : DbContext, IUnitOfWork
    {
        public CidadaoContext(DbContextOptions<CidadaoContext> options) : base(options) { }

        public DbSet<Contribuicao> Contribuicao { get; set; }
        public DbSet<Impostos> Impostos { get; set; }
        public DbSet<StatusContribuicao> StatusContribuicao { get; set; }
        public DbSet<Domain.Entities.Cidadao> Cidadao { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckIfDatabaseExists() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Domain.Entities.Cidadao>().OwnsOne(p => p.CPF);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
