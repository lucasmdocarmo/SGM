using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Consulta
{
    public class Agendamentos:BaseEntity
    {
        public Agendamentos(DateTime dataConsulta, Guid medicoId)
        {
            DataConsulta = dataConsulta;
            MedicoId = medicoId;
            Reservado = true;
        }

        public DateTime DataConsulta { get; set; }
        public Guid MedicoId { get; set; }
        public Medicos Medico { get; set; }
        public bool Reservado { get; set; }
    }
}
