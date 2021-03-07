﻿using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Domain.Entities.Consulta;
using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public class Consultas: BaseEntity, IAggregateRoot
    {
        public string Especialidade { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesMedicas { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public bool Confirmada { get; set; }

        public Prescricao Prescricao { get; set; }
        public Paciente Paciente { get; private set; }
        public Guid PacienteId { get; private set; }
        public Medicos Medico { get; set; }
        public Guid MedicoId { get; set; }

    }
}
