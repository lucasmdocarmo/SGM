using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Domain.Entities.Contribuinte
{
    public sealed class Contribuicao : BaseEntity
    {
        internal Contribuicao() { }

        public string CodigoGuiaContribuicao { get; set; }
        public string AnoFiscal { get; set; }
        public decimal TotalImpostos { get; set; }
        public DateTime Pagamento { get; set; }
        public Guid ImpostoId { get; set; }
        public Impostos Impostos { get; set; }
        public Guid CidadaoId { get; set; }
        public Cidadao Cidadao {get;set;}
        public StatusContribuicao Contribuinte { get; set; }
        public Guid ContribuinteId { get; set; }
    }
}
