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
            builder.Property(x => x.Custo).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.Especialidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.InformacoesMedicas).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataConsulta).HasColumnType("datetime").IsRequired();

            builder.HasOne(c => c.Clinicas).WithMany(p => p.Consultas).HasForeignKey(p => p.ClinicaId);
            builder.HasOne(c => c.Medicos).WithMany(p => p.Consultas).HasForeignKey(p => p.MedicosId);
            builder.HasOne(c => c.Paciente).WithMany(p => p.Consultas).HasForeignKey(p => p.PacienteId);

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
            builder.Property(x => x.CodigoCidadao).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.InformacoesMedicas).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DetalhesPaciente).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int").IsRequired();
            builder.OwnsOne(x => x.CPF);
            builder.Property(x => x.Sexo);

            builder.ToTable("Paciente");
        }
    }
    public class AtendimentosMapping : IEntityTypeConfiguration<Atendimentos>
    {
        public void Configure(EntityTypeBuilder<Atendimentos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.DataConsulta).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Compareceu);
            builder.Property(x => x.CustoTotal).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.TotalPago).HasColumnType("decimal(10,2)").IsRequired();
            builder.HasOne(c => c.Consulta).WithOne(p => p.Atendimentos).HasForeignKey<Atendimentos>(x => x.ConsultaId);

            builder.ToTable("Atendimentos");
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
            builder.HasOne(c => c.Atendimentos).WithOne(p => p.prescricao).HasForeignKey<Prescricao>(x => x.AtendimentoId);

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
            builder.HasOne(c => c.Endereco).WithOne(p => p.Clinica).HasForeignKey<Endereco>(x => x.ClinicaId);
            builder.HasMany(c => c.Consultas).WithOne(p => p.Clinicas).HasForeignKey(x => x.ClinicaId);
            builder.HasMany(c => c.Medicos).WithOne(p => p.Clinica).HasForeignKey(x => x.ClinicaId);

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
            builder.Property(x => x.HoraInicio).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.HoraFim).HasColumnType("datetime").IsRequired();

            builder.HasOne(c => c.Clinica).WithMany(p => p.Medicos).HasForeignKey(x => x.ClinicaId);
            builder.HasOne(c => c.Endereco).WithMany(p => p.Medicos).HasForeignKey(x => x.EnderecoId);

            builder.ToTable("Endereco");
        }
    }

}
