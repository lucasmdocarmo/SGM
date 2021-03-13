using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Domain.Entities.Contribuinte
{
    public sealed class StatusContribuicao : BaseEntity
    {
        public StatusContribuicao(string codigoGuiaContribuicao, ETipoStatusContribuinte status, DateTime registroInicial, 
            DateTime registroFinal, bool finalizado)
        {
            CodigoGuiaContribuicao = codigoGuiaContribuicao;
            Status = status;
            RegistroInicial = registroInicial;
            RegistroFinal = registroFinal;
            Finalizado = finalizado;
        }

        internal StatusContribuicao() { }
        public string CodigoGuiaContribuicao { get; set; }
        public ETipoStatusContribuinte Status { get; set; }
        public DateTime RegistroInicial { get; set; }
        public DateTime RegistroFinal { get; set; }
        public bool Finalizado { get; set; }
        public IReadOnlyCollection<Contribuicao> Contribuicao { get; set; }

        public void EditarStatus(string codigoGuiaContribuicao, ETipoStatusContribuinte status, DateTime registroInicial,
            DateTime registroFinal, bool finalizado)
        {
            CodigoGuiaContribuicao = codigoGuiaContribuicao;
            Status = status;
            RegistroInicial = registroInicial;
            RegistroFinal = registroFinal;
            Finalizado = finalizado;
        }
    }
    public enum ETipoStatusContribuinte
    {
        Cobrado = 1,
        EmAndamento = 2,
        Pago = 3,
        Cancelado = 4,
        Divida = 5
    }
}
