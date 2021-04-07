using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Manager.Domain.Entities;
using SGM.Manager.Domain.Entities.Integration;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Context
{
    public class DepartamentoMapping : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Codigo).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
        
            builder.HasMany(c => c.Funcionario).WithOne(p => p.Departamento)
                .HasForeignKey(p => p.DepartamentoId);

            builder.HasMany(c => c.Usuario).WithOne(p => p.Departamento)
                .HasForeignKey(p => p.DepartamentoId).IsRequired(false);

            builder.ToTable("Departamento");
        }
    }
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Login).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.TipoFuncionario).HasColumnType("int").IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();

            builder.ToTable("Funcionario");
        }
    }
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Login).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.TipoUsuario).HasColumnType("int").IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();


            builder.ToTable("Usuario");
        }
    }
    public class CidadaoUserMapping : IEntityTypeConfiguration<CidadaoUser>
    {
        public void Configure(EntityTypeBuilder<CidadaoUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Login).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();

            builder.ToTable("CidadaoUser");
        }
    }
    public class AppIntegrationMapping : IEntityTypeConfiguration<AppIntegration>
    {
        public void Configure(EntityTypeBuilder<AppIntegration> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.ApiKey).IsRequired();
            builder.Property(x => x.AppIntegrationCode).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Sistema).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.SistemaRaiz);

            builder.ToTable("AppIntegrations");
        }
    }
}
