using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Text;
using SGM.Cidadao.Domain.Entities;

namespace SGM.Cidadao.Infra.Context
{
    public class ContribuinteMapping : IEntityTypeConfiguration<Contribuinte>
    {
        public void Configure(EntityTypeBuilder<Contribuinte> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.AnoFiscal).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.TotalImpostos).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Pagamento).HasColumnType("datetime").IsRequired();

            builder.HasMany(c => c.Impostos).WithOne(p => p.Contribuinte)
                .HasForeignKey(p => p.ContribuinteId);
           

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
         
            builder.Property(x => x.DataFinal).HasColumnType("datetime").IsRequired();

            builder.HasMany(c => c.Cidadao).WithMany(p => p.Impostos)
                .UsingEntity(c => c.ToTable("ImpostosCidadao"));
        }
    }

    public class CidadaoMapping : IEntityTypeConfiguration<Cidadaos>
    {
        public void Configure(EntityTypeBuilder<Cidadaos> builder)
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
            builder.HasOne(x => x.Cidadao).WithOne(x => x.Endereco).HasForeignKey<Endereco>(x => x.CidadaoId);

            builder.ToTable("Endereco");
        }
    }
}
