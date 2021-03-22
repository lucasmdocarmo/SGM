using SGM.Cidadao.Domain.Entities.Contribuinte;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Queries.Status
{
    public class StatusImpostoQueryResult
    {
        public StatusImpostoQueryResult(IList<StatusContribuicao> status)
        {
            Status = status;
            CidadaoStatus = new List<CidadaoStatus>();
            GetLista(status);
        }

        public IList<StatusContribuicao> Status { get; set; }
        public IList<CidadaoStatus> CidadaoStatus { get; set; }

        public IList<CidadaoStatus> GetLista(IList<StatusContribuicao> status)
        {
            foreach (var item in status)
            {
                CidadaoStatus.Add(new CidadaoStatus()
                {
                    Finalizado = item.Finalizado,
                    RegistroInicial = item.RegistroInicial,
                    RegistroFinal = item.RegistroFinal,
                    Status = Enum.GetName(typeof(ETipoStatusContribuinte), item.Status),
                });
            }
            return CidadaoStatus;
        }
    }
    public class CidadaoStatus
    {
        public DateTime? RegistroInicial { get; set; }
        public string Status { get; set; }
        public DateTime? RegistroFinal { get; set; }
        public bool? Finalizado { get; set; }
    }
}
