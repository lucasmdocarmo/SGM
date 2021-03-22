using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Domain.Entities.Contribuinte
{
    public sealed class Contribuicao : BaseEntity, IAggregateRoot
    {
        public Contribuicao(string codigoGuiaContribuicao, string anoFiscal, decimal totalImpostos, DateTime? pagamento, 
                            Guid cidadaoId, Guid impostoId, Guid contribuinteId)
        {
            CodigoGuiaContribuicao = codigoGuiaContribuicao;
            AnoFiscal = anoFiscal;
            TotalImpostos = totalImpostos;
            Pagamento = pagamento;
            CidadaoId = cidadaoId;
            ImpostoId = impostoId;
            ContribuinteId = contribuinteId;
        }

        internal Contribuicao() { }

        public string CodigoGuiaContribuicao { get; set; }
        public string AnoFiscal { get; set; }
        public decimal TotalImpostos { get; set; }
        public DateTime? Pagamento { get; set; }

        public Guid? ImpostoId { get; set; }
        public Impostos Impostos { get; set; }

        public Guid? CidadaoId { get; set; }
        public Cidadao Cidadao {get;set;}

        public StatusContribuicao Contribuinte { get; set; }
        public Guid? ContribuinteId { get; set; }

        public void EditarContribuinte(string codigoGuiaContribuicao, string anoFiscal, decimal totalImpostos, DateTime pagamento,
                            Guid cidadaoId, Guid impostoId, Guid contribuinteId)
        {
            this.CodigoGuiaContribuicao = codigoGuiaContribuicao;
            this.AnoFiscal = anoFiscal;
            this.TotalImpostos = totalImpostos;
            this.Pagamento = pagamento;
            this.CidadaoId = cidadaoId;
            this.ImpostoId = impostoId;
            this.ContribuinteId = contribuinteId;
        }
        public void VincularStatus(Guid idContribuicao)
        {
            this.ContribuinteId = idContribuicao;
        }
    }
}
