using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SGM.Manager.Infra.Repositories
{
    public class FuncionarioRepository : BaseRepository<Funcionario, ManagerContext>, IFuncionarioRepository
    {
        public FuncionarioRepository(ManagerContext db) : base(db) { }

        public async Task<Funcionario> GetFuncionario(string login, string senha)
        {
            var result = await base.Search(user => user.Login == login && user.Senha == senha).ConfigureAwait(true);

            return result.FirstOrDefault();
        }
    }
}
