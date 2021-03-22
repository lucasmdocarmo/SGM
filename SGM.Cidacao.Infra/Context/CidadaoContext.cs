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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var cidadao = new Domain.Entities.Cidadao("Lucas Mariano", Convert.ToDateTime("03-27-1992"), "11640810633",
                "13944854", true, "lucas@email.com", "dev", "234234", "234234234");

            var imposto = new Impostos(1000M, Domain.ValueObjects.ETipoImposto.IPTU, Convert.ToDateTime("01-12-2021"), "2021");
            var StatusContribuicao = new StatusContribuicao("CONTR-001", ETipoStatusContribuinte.EmAndamento, DateTime.Now, null, false);

            var contribuicao = new Contribuicao("TRIB-001", "2021", 30000M, null, cidadao.Id, imposto.Id, StatusContribuicao.Id);

            modelBuilder.Entity<Domain.Entities.Cidadao>().HasData(cidadao);
            modelBuilder.Entity<Impostos>().HasData(imposto);
            modelBuilder.Entity<StatusContribuicao>().HasData(StatusContribuicao);
            modelBuilder.Entity<Contribuicao>().HasData(contribuicao);

        }
    }
}
