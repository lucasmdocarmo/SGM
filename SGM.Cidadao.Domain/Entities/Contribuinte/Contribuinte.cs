using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Domain.Entities.Contribuinte
{
    public sealed class Contribuinte :BaseEntity
    {
        public string AnoFiscal { get; set; }
        public string TotalImpostos { get; set; }
        public DateTime Pagamento { get; set; }

        public Impostos Impostos { get; set; }
        public Guid ImpostosId { get; set; }
        public Cidadao Cidadao { get; set; }
        public Guid CidadaoId { get; set; }
    }
}
