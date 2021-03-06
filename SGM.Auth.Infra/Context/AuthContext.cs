using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SGM.Auth.Domain.Entities;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Auth.Infra.Context
{
    public class AuthContext : DbContext, IUnitOfWork
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckDatabaseStatus() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
