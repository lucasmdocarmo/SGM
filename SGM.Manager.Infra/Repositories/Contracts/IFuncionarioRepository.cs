using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Manager.Infra.Repositories.Contracts
{
    public interface IFuncionarioRepository : IRepository<Funcionario, ManagerContext>
    {
        Task<Funcionario> GetFuncionario(string login, string senha);
    }
}
