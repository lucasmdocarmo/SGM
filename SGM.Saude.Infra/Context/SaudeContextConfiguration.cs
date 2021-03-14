using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Saude.Domain.Entities;
using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Domain.Entities.Consulta;
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
            builder.Property(x => x.Especialidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Confirmada);
            builder.Property(x => x.InformacoesMedicas).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataConsulta).HasColumnType("datetime").IsRequired();

            builder.ToTable("Consultas");
        }
    }
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataNascimento).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Identidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.InformacoesMedicas).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DetalhesPaciente).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int").IsRequired();
            builder.OwnsOne(x => x.CPF);
            builder.Property(x => x.Sexo);

            builder.HasMany(x => x.Consultas).WithOne(x => x.Paciente).HasForeignKey(x => x.PacienteId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Paciente");
        }
    }
    public class PrescricaoMapping : IEntityTypeConfiguration<Prescricao>
    {
        public void Configure(EntityTypeBuilder<Prescricao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Detalhes).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Resumo).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Retornar);
            builder.Property(x => x.DataInicial).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Validade).HasColumnType("datetime").IsRequired();

            builder.HasOne(x => x.Consulta).WithOne(x => x.Prescricao).HasForeignKey<Prescricao>(x => x.ConsultaId);

            builder.ToTable("Prescricao");
        }
    }
    public class ClinicaMapping : IEntityTypeConfiguration<Clinica>
    {
        public void Configure(EntityTypeBuilder<Clinica> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Telefone).HasMaxLength(250).HasColumnType("varchar(50)").IsRequired();

            builder.HasMany(x => x.Medicos).WithOne(x => x.Clinica).HasForeignKey(x => x.ClinicaId);

            builder.ToTable("Clinica");
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

            builder.HasOne(x => x.Medico).WithOne(x => x.Endereco).HasForeignKey<Endereco>(x => x.MedicoId);

            builder.ToTable("Endereco");
        }
    }
    public class MedicosMapping : IEntityTypeConfiguration<Medicos>
    {
        public void Configure(EntityTypeBuilder<Medicos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Profissao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CRM).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Especialidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.AtendeSegunda);
            builder.Property(x => x.AtendeTerca);
            builder.Property(x => x.AtendeQuarta);
            builder.Property(x => x.AtendeQuinta);
            builder.Property(x => x.AtendeSexta);
            builder.Property(x => x.AtendeSabado);
            builder.Property(x => x.AtendeDomingo);
            builder.Property(x => x.Ativo);
            builder.Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Telefone).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.ValorHora).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.HoraInicio);
            builder.Property(x => x.HoraFim);
            builder.HasMany(x => x.Consultas).WithOne(x => x.Medico).HasForeignKey(x => x.MedicoId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable("Medicos");
        }
    }

}
