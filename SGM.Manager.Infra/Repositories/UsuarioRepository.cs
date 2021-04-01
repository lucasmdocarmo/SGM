using SGM.Manager.Domain.Entities;
using SGM.Manager.Infra.Context;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SGM.Shared.Core.Entity;

namespace SGM.Manager.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario, ManagerContext>, IUsuarioRepository
    {
        public UsuarioRepository(ManagerContext db) : base(db) { }

        public async Task<Usuario> GetUsuario(string login, string senha)
        {
            var result = await base.Search(user => user.Login == login && user.Senha == senha).ConfigureAwait(true);

            return result.FirstOrDefault();
        }
    }
    public class CidadaoUserRepository : BaseRepository<CidadaoUser, ManagerContext>, ICidadaoUserRepository
    {
        public CidadaoUserRepository(ManagerContext db) : base(db) { }

        public async Task<CidadaoUser> GetCidadaoUser(string login, string senha)
        {
            var result = await base.Search(user => user.Login == login && user.Senha == senha).ConfigureAwait(true);

            return result.FirstOrDefault();
        }
    }
}
