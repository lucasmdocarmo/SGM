using SGM.Cidadao.Domain.Entities;
using SGM.Cidadao.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Repositories.Contracts
{
    public interface IEnderecoRepository : IRepository<Endereco, CidadaoContext>
    {
    }
}
