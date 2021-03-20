using SGM.Manager.Domain.Entities.Integration;
using SGM.Manager.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Repositories.Contracts
{
    public interface IIntegracaoRepository : IRepository<AppIntegration, ManagerContext>
    {
    }
}
