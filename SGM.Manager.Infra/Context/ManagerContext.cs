using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SGM.Manager.Domain.Entities;
using SGM.Manager.Domain.Entities.Integration;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Manager.Infra.Context
{
    public class ManagerContext : DbContext, IUnitOfWork
    {
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<AppIntegration> AppIntegration { get; set; }

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
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var departamento = new Departamento("Clinica", "CLI001");
            var departamento1 = new Departamento("Saude", "SAU001");
            var departamento2 = new Departamento("Gerencia", "GER001");
            var departamento3 = new Departamento("Presidencia", "GER002");

            modelBuilder.Entity<Departamento>().HasData(departamento, departamento1, departamento2, departamento3);

            var user = new Usuario("Lucas Mariano", "123456", "lucasmdc", Shared.Core.ValueObjects.ETipoUsuario.Administrador, departamento.Id);
            var user2 = new Usuario("Sebastiao", "123456", "sebastic", Shared.Core.ValueObjects.ETipoUsuario.Comum, departamento2.Id);
            var user3 = new Usuario("Steve Rogers", "123456", "america", Shared.Core.ValueObjects.ETipoUsuario.Gerente, departamento3.Id);

            modelBuilder.Entity<Usuario>().HasData(user, user2);

            var func = new Funcionario("lucas","lucasm", "123456", Shared.Core.ValueObjects.ETipoFuncionario.Clinica, departamento.Id);
            var func2 = new Funcionario("sebastiao", "sebasx", "123456", Shared.Core.ValueObjects.ETipoFuncionario.Gestao, departamento3.Id);

            modelBuilder.Entity<Funcionario>().HasData(func, func2);

        }
    }
}
