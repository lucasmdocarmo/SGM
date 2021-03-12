using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Repositories
{
    public class FuncionarioRepository : BaseRepository<Funcionario, ManagerContext>, IFuncionarioRepository
    {
        public FuncionarioRepository(ManagerContext db) : base(db) { }
    }
}
