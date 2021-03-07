using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Manager.Domain.Entities;
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
                .HasForeignKey(p => p.DepartamentoId);

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



            builder.ToTable("Usuario");
        }
    }
}
