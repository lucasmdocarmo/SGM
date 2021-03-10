using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories.Contracts
{
    public interface IImpostosRepository : IRepository<Impostos, CidadaoContext>
    {
    }
}
