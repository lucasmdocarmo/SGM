using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Entity
{
    public class CidadaoUser : BaseEntity
    {
        public CidadaoUser(string nome, string senha, string login)
        {
            Nome = nome;
            Senha = senha;
            Login = login;
        }

        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Login { get; private set; }
    }
}
