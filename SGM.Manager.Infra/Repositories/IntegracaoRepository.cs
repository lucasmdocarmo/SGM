using SGM.Manager.Domain.Entities.Integration;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Repositories
{
    public class IntegracaoRepository : BaseRepository<AppIntegration, ManagerContext>, IIntegracaoRepository
    {
        public IntegracaoRepository(ManagerContext db) : base(db) { }
    }
}
