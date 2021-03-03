using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Consulta
{
    public sealed class Prescricao :BaseEntity
    {
        public string Detalhes { get; set; }
        public bool Retornar { get; set; }
        public string Resumo { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime Validade { get; set; }

        public Guid ClinicaId { get; set; }
        public Guid AtendimentoId { get; set; }
        public Guid MedicoslId { get; set; }
        public Guid PacienteId { get; set; }

    }
}
