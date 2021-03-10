using SGM.Saude.Domain.Entities.Consulta;
using SGM.Saude.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Infra.Repositories.Contracts
{
    public interface IPrescricaoRepository : IRepository<Prescricao, SaudeContext>
    {
    }
}
