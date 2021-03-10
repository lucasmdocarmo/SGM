using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories
{
    public class StatusContribuicaoRepository : BaseRepository<StatusContribuicao, CidadaoContext>, IStatusContribuicaoRepository
    {
        protected StatusContribuicaoRepository(CidadaoContext db) : base(db) { }

    }
}
