using SGM.Cidadao.Domain.ValueObjects;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SGM.Cidadao.Domain.Entities.Contribuinte
{
    public sealed class Impostos : BaseEntity
    {
        public decimal Tributo { get; private set; }
        public ETipoImposto TipoImposto { get; private set; }
        public DateTime DataFinal { get; private set; }
        public string AnoFiscal { get; private set; }
        public IReadOnlyCollection<Contribuicao> Contribuicao { get; set; }

    }
}
