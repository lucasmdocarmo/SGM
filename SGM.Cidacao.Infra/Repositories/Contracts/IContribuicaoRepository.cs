using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Infra.Repositories.Contracts
{
    public interface IContribuicaoRepository: IRepository<Contribuicao, CidadaoContext>
    {
        Task<List<ListImpostosContribuicao>> GetCidadao(string cpf);
    }
}
