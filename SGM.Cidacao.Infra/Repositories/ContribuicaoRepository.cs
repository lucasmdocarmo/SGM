using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories
{
    public class ContribuicaoRepository : BaseRepository<Contribuicao, CidadaoContext>, IContribuicaoRepository
    {
        protected ContribuicaoRepository(CidadaoContext db) : base(db) { }

    }
}
