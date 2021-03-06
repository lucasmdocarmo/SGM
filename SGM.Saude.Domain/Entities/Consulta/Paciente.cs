using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Paciente : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public CPF CPF { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public string CodigoCidadao { get; set; }
        public ETipoStatusPaciente Status { get; set; }
        public string DetalhesPaciente { get; set; }
        public string InformacoesMedicas { get; set; }

        public IReadOnlyCollection<Consultas> Consultas { get; set; }
    }
}
