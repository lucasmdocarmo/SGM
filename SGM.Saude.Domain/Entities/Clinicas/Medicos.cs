using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Domain.Entities.Consulta;
using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Medicos :BaseEntity, IAggregateRoot
    {
        internal Medicos() { }
        public Medicos(string nome, string profissao, string cRM, string especialidade, TimeSpan horaInicio,
            TimeSpan horaFim, bool ativo, string email, string telefone, decimal valorHora, bool atendeTodosOsDias, Guid clinicaId)
        {
            Nome = nome;
            Profissao = profissao;
            CRM = cRM;
            Especialidade = especialidade;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Ativo = ativo;
            Email = email;
            Telefone = telefone;
            ValorHora = valorHora;
            AtendeTodosOsDias = atendeTodosOsDias;

            if (AtendeTodosOsDias)
            {
                AtendeSegunda = true;
                AtendeTerca = true;
                AtendeQuarta = true;
                AtendeQuinta = true;
                AtendeSexta = true;
                AtendeSabado = true;
            }
            ClinicaId = clinicaId;
        }
        public Medicos(string nome, string profissao, string cRM, string especialidade, TimeSpan horaInicio,
           TimeSpan horaFim, bool ativo, string email, string telefone, decimal valorHora, Guid clinicaId, bool atendeSegunda, bool atendeTerca, 
           bool atendeQuarta, bool atendeQuinta, bool atendeSexta, bool atendeSabado)
        {
            Nome = nome;
            Profissao = profissao;
            CRM = cRM;
            Especialidade = especialidade;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Ativo = ativo;
            Email = email;
            Telefone = telefone;
            ValorHora = valorHora;
            AtendeSegunda = atendeSegunda;
            AtendeTerca = atendeTerca;
            AtendeQuarta = atendeQuarta;
            AtendeQuinta = atendeQuinta;
            AtendeSexta = atendeSexta;
            AtendeSabado = atendeSabado;
            ClinicaId = clinicaId;
        }
        public bool AtendeTodosOsDias { get; set; }
        public string Nome { get; private set; }
        public string Profissao { get; private set; }
        public string CRM { get; private set; }
        public string Especialidade { get; private set; }
        public bool AtendeSegunda { get; private set; }
        public bool AtendeTerca { get; private set; }
        public bool AtendeQuarta { get; private set; }
        public bool AtendeQuinta { get; private set; }
        public bool AtendeSexta { get; private set; }
        public bool AtendeSabado { get; private set; }
        public bool AtendeDomingo { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }
        public bool Ativo { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public decimal ValorHora { get; private set; }

        public Clinica Clinica { get; set; }
        public Endereco Endereco { get; set; }
        public Guid ClinicaId { get; set; }
        public IList<Consultas> Consultas { get; set; }
        public IList<Agendamentos> Agendamentos { get; set; }
        public void EditarMedico(string nome, string profissao, string cRM, string especialidade, TimeSpan horaInicio,
           TimeSpan horaFim, bool ativo, string email, string telefone, decimal valorHora, bool atendeSegunda, bool atendeTerca,
           bool atendeQuarta, bool atendeQuinta, bool atendeSexta, bool atendeSabado)
        {
            Nome = nome;
            Profissao = profissao;
            CRM = cRM;
            Especialidade = especialidade;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Ativo = ativo;
            Email = email;
            Telefone = telefone;
            ValorHora = valorHora;
            AtendeSegunda = atendeSegunda;
            AtendeTerca = atendeTerca;
            AtendeQuarta = atendeQuarta;
            AtendeQuinta = atendeQuinta;
            AtendeSexta = atendeSexta;
            AtendeSabado = atendeSabado;
        }
    }
}
