using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SGM.Cidadao.Domain.ValueObjects;

namespace SGM.Cidadao.Infra.Repositories
{
    public class ContribuicaoRepository : BaseRepository<Contribuicao, CidadaoContext>, IContribuicaoRepository
    {
        public ContribuicaoRepository(CidadaoContext db) : base(db) { }
        public List<ListImpostosContribuicao> Contriuicoes { get; set; }
        public async Task<List<ListImpostosContribuicao>> GetCidadao(string cpf)
        {
            var result = base.Db.Cidadao.Where(x => x.CPF == cpf).FirstOrDefault().Id;
            var list = base.Db.Contribuicao.Where(x => x.CidadaoId == result).ToList();
            Contriuicoes = new List<ListImpostosContribuicao>();

            foreach (var item in list)
            {
                Contriuicoes.Add(new ListImpostosContribuicao()
                {
                    AnoFiscal = item.AnoFiscal,
                    Codigo = item.CodigoGuiaContribuicao,
                    Pagamento = item.Pagamento,
                    TotalImpostos = item.TotalImpostos,
                    TipoImposto = Enum.GetName(typeof(ETipoImposto), base.Db.Impostos.Where(x => x.Id == item.ImpostoId).FirstOrDefault().TipoImposto),
                });
            }
            return Contriuicoes;
        }


    }
    public class ListImpostosContribuicao
    {

        public string Codigo { get; set; }
        public string AnoFiscal { get; set; }
        public decimal TotalImpostos { get; set; }
        public string TipoImposto { get; set; }
        public DateTime? Pagamento { get; set; }
    }
}
