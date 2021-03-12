using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories
{
    public class CidadaoRepository : BaseRepository<Domain.Entities.Cidadao, CidadaoContext>, ICidadaoRepository
    {
        public CidadaoRepository(CidadaoContext db) : base(db) { }


    }
}
