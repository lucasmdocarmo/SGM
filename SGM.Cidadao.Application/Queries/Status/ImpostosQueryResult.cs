using SGM.Cidadao.Domain.Entities.Contribuinte;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Queries.Status
{
    public class ImpostosQueryResult
    {
        public ImpostosQueryResult(List<Contribuicao> contriuicoes)
        {
            Contriuicoes = contriuicoes;
            Lista = new List<ListaImposos>();
            GetList(contriuicoes);
        }

        public List<ListaImposos> Lista { get; set; }
        public List<Contribuicao> Contriuicoes { get; set; }

        public List<ListaImposos> GetList(List<Contribuicao> Contriuicoes)
        {
            foreach (var item in Contriuicoes)
            {
                Lista.Add(new ListaImposos()
                {
                    AnoFiscal = item.AnoFiscal,
                    Codigo = item.CodigoGuiaContribuicao,
                    Pagamento = item.Pagamento,
                    TotalImpostos = item.TotalImpostos
                });
            }
            return Lista;
        }
    }
    public class ListaImposos
    {
        public string Codigo { get; set; }
        public string AnoFiscal { get; set; }
        public decimal TotalImpostos { get; set; }
        public string TipoImposto { get; set; }
        public DateTime? Pagamento { get; set; }
    }
}
