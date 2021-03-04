using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Medicos :BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public bool AtendeSegunda { get; set; }
        public bool AtendeTerca { get; set; }
        public bool AtendeQuarta { get; set; }
        public bool AtendeQuinta { get; set; }
        public bool AtendeSexta { get; set; }
        public bool AtendeSSabado { get; set; }
        public bool AtendeDomingo { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public decimal ValorHora { get; set; }

        public Clinica Clinica { get; set; }
        public Guid ClinicaId { get; set; }

        public Endereco Endereco { get; set; }
        public Guid EnderecoId { get; set; }
    }
}
