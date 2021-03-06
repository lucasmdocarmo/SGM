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
        public decimal Custo { get; private set; }
        public string Especialidade { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesMedicas { get; private set; }
        public DateTime DataConsulta { get; private set; }

        public Guid ClinicaId { get; private set; }
        public Clinica Clinicas { get; private set; }
        public Medicos Medicos { get; private set; }
        public Guid MedicosId { get; private set; }
        public Paciente Paciente { get; private set; }
        public Guid PacienteId { get; private set; }
        public Atendimentos Atendimentos { get; set; }

    }
}
