using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Gestao.Domain.Entities;
using SGM.Gestao.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Infra.Context
{
    public class FuncionariosMapping : IEntityTypeConfiguration<Funcionarios>
    {
        public void Configure(EntityTypeBuilder<Funcionarios> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Ativo);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            
            builder.Property(x => x.DataInicio).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DataTermino).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.TipoFuncao).HasColumnType("int").IsRequired();

            builder.OwnsOne(x => x.CPF);

            builder.ToTable("Funcionarios");
        }
    }
    public class InstituicoesMapping : IEntityTypeConfiguration<Instituicoes>
    {
        public void Configure(EntityTypeBuilder<Instituicoes> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.RazaoSocial).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Responsavel).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CNPJ).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.TipoInstituicao).HasColumnType("int").IsRequired();

            builder.HasMany(c => c.Funcionarios).WithOne(p => p.Instituicao).HasForeignKey(x => x.InstituicaoId);

            builder.ToTable("Funcionarios");
        }
    }
    public class MunicipioMapping : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Prefeito).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();

            builder.HasMany(c => c.Instituicoes).WithOne(p => p.Municipio).HasForeignKey(x => x.MunicipioId);
            builder.HasMany(c => c.Projetos).WithOne(p => p.Municipio).HasForeignKey(x => x.MunicioId);

            builder.ToTable("Municipio");
        }
    }
    public class ProjetosMapping : IEntityTypeConfiguration<Projetos>
    {
        public void Configure(EntityTypeBuilder<Projetos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CodigoProjeto).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataEntrega).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DataInicio).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.TipoStatus).HasColumnType("int").IsRequired();
            builder.Property(x => x.TipooCategoria).HasColumnType("int").IsRequired();
            builder.Property(x => x.Custo).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasMany(c => c.Colaboradores).WithOne(p => p.Projetos).HasForeignKey(x => x.ProjetosId);
            builder.HasMany(c => c.Informacoes).WithOne(p => p.Projeto).HasForeignKey(x => x.ProjetoId);

            builder.ToTable("Projetos");
        }
    }
    public class InformacoesMapping : IEntityTypeConfiguration<Informacoes>
    {
        public void Configure(EntityTypeBuilder<Informacoes> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Descricao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.ToTable("Informacoes");
        }
    }
    public class ColaboradorMapping : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.TipoFuncao).HasColumnType("int").IsRequired();
            builder.Property(x => x.TipoColaborador).HasColumnType("int").IsRequired();
            builder.OwnsOne(x => x.CPF);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
          
            builder.ToTable("Colaborador");
        }
    }
}
