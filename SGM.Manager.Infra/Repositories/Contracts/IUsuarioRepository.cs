using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Manager.Infra.Repositories.Contracts
{
    public interface IUsuarioRepository : IRepository<Usuario, ManagerContext>
    {
        Task<Usuario> GetUsuario(string login, string senha);
    }
    public interface ICidadaoUserRepository : IRepository<CidadaoUser, ManagerContext>
    {
        Task<CidadaoUser> GetCidadaoUser(string login, string senha);
    }
}
