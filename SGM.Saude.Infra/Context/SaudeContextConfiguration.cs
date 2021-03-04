using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Saude.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Context
{
    public class ConsultasMapping : IEntityTypeConfiguration<Consultas>
    {
        public void Configure(EntityTypeBuilder<Consultas> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.CustoTotal).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.Especialidade).HasMaxLength(10).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.Informacoes).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.DataConsulta).HasColumnType("datetime").IsRequired();

            builder.HasOne(c => c.Cliente).WithMany(p => p.Contratos)
               .HasForeignKey(p => p.ClienteId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbContrato");
        }
    }
}
