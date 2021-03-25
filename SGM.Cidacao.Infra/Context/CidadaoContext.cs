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
            var cidadao = new Domain.Entities.Cidadao("Lucas Mariano", Convert.ToDateTime("03-27-1992"), "11640810633", "13944854", true, "lucas@email.com", "dev", "234234", "234234234");

            var cidadao2 = new Domain.Entities.Cidadao("Joao Da Silva", Convert.ToDateTime("1995-03-11"), "11506871102", "11323123", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao3 = new Domain.Entities.Cidadao("Ronaldo Coelho", Convert.ToDateTime("1998-03-17"), "11502321102", "1231231", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao4 = new Domain.Entities.Cidadao("Cesar Junior", Convert.ToDateTime("1977-03-17"), "11211221102", "123123123", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao5 = new Domain.Entities.Cidadao("Igor da Silva", Convert.ToDateTime("1988-03-17"), "11623233233", "456456456", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao6 = new Domain.Entities.Cidadao("Sebastiao souza", Convert.ToDateTime("03-27-1992"), "98987899633", "456456456", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao7 = new Domain.Entities.Cidadao("Arnold Pinto", Convert.ToDateTime("2001-03-17"), "11621123633", "678686778", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao8 = new Domain.Entities.Cidadao("Sergio da silva", Convert.ToDateTime("2001-03-17"), "11640810633", "13944854", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao9 = new Domain.Entities.Cidadao("Vinicius da silva", Convert.ToDateTime("1966-03-17"), "11211230633", "6786786", true, "lucas@email.com", "dev", "234234", "234234234");
            var cidadao10 = new Domain.Entities.Cidadao("Everton da Silva", Convert.ToDateTime("1976-03-17"), "21640812632", "978967573", true, "lucas@email.com", "dev", "234234", "234234234");

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
