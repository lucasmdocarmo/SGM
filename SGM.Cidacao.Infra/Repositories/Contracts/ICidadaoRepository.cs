using SGM.Cidadao.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories.Contracts
{
    public interface ICidadaoRepository : IRepository<Domain.Entities.Cidadao, CidadaoContext>
    {
    }
}
