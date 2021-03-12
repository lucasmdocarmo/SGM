using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento, ManagerContext>, IDepartamentoRepository
    {
        public DepartamentoRepository(ManagerContext db) : base(db) { }
    }
}
