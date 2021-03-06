using SGM.Saude.Domain.Entities.Consulta;
using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Atendimentos : BaseEntity, IAggregateRoot
    {
        public DateTime DataConsulta { get; private set; }
        public bool Compareceu { get; private set; }
        public decimal CustoTotal { get; private set; }
        public decimal TotalPago { get; private set; }
        public Consultas Consulta { get; private set; }
        public Guid ConsultaId { get; private set; }
        public Prescricao prescricao { get; private set; }
    }
}
