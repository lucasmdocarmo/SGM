using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Atendimentos : BaseEntity, IAggregateRoot
    {
        public DateTime DataConsulta { get; set; }
        public bool Compareceu { get; set; }
        public decimal CustoTotal { get; set; }
        public decimal TotalPago { get; set; }
        public Guid PacienteId { get; set; }
        public Guid ClinicaId { get; set; }
        public Guid MedicoslId { get; set; }
        public Guid ConsultaId { get; set; }
    }
}
