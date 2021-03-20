using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using System;
using System.Collections.Generic;
using System.Text;
using SGM.Cidadao.Domain.Entities;

namespace SGM.Cidadao.Infra.Context
{
    public class ContribuinteMapping : IEntityTypeConfiguration<Contribuicao>
    {
        public void Configure(EntityTypeBuilder<Contribuicao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.AnoFiscal).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CodigoGuiaContribuicao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.TotalImpostos).HasMaxLength(250).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.Pagamento).HasColumnType("datetime").IsRequired();

            builder.HasOne(x => x.Impostos).WithMany(x => x.Contribuicao).HasForeignKey(x => x.ImpostoId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(x => x.Cidadao).WithMany(x => x.Contribuicao).HasForeignKey(x => x.CidadaoId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
            builder.HasOne(x => x.Contribuinte).WithMany(x => x.Contribuicao).HasForeignKey(x => x.ContribuinteId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);

            builder.ToTable("Contribuinte");
        }
    }
    public class ImpostosMapping : IEntityTypeConfiguration<Impostos>
    {
        public void Configure(EntityTypeBuilder<Impostos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Tributo).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.TipoImposto).HasColumnType("int").IsRequired();
            builder.Property(x => x.AnoFiscal).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataFinal).HasColumnType("datetime").IsRequired();
        }
    }

    public class ContribuicaoMapping : IEntityTypeConfiguration<StatusContribuicao>
    {
        public void Configure(EntityTypeBuilder<StatusContribuicao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.CodigoGuiaContribuicao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int").IsRequired();
            builder.Property(x => x.RegistroFinal).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.RegistroInicial).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Finalizado);
           
        }
    }

    public class CidadaoMapping : IEntityTypeConfiguration<Domain.Entities.Cidadao>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Cidadao> builder)
        {
            builder.HasKey(c => c.Id);
           
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Identidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Sexo);
            builder.Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Profissao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CodigoCidadao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Telefone).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Celular).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.OwnsOne(X => X.CPF);

            builder.ToTable("Cidadaos");
        }
    }
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.CEP).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Logradouro).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Numero).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Complemento).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Estado).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.HasOne(x => x.Cidadao).WithOne(x => x.Endereco).HasForeignKey<Endereco>(x => x.CidadaoId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Endereco");
        }
    }
}
