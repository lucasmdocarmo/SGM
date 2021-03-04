using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public class Consultas: BaseEntity, IAggregateRoot
    {
        public decimal CustoTotal { get; set; }
        public string Especialidade { get; set; }
        public string Descricao { get; set; }
        public string InformacoesMedicas { get; set; }
        public DateTime DataConsulta { get; set; }

        public Guid ClinicaId { get; set; }
        public Clinica Clinicas { get; set; }
        public Medicos Medicos { get; set; }
        public Guid MedicoslId { get; set; }

    }
}
