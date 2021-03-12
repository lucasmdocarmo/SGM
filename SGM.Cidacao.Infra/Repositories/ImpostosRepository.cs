using SGM.Cidadao.Domain.Entities;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories
{
    public class ImpostosRepository : BaseRepository<Impostos, CidadaoContext>, IImpostosRepository
    {
        public ImpostosRepository(CidadaoContext db) : base(db) { }
    }
}
