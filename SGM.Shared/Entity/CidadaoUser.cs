using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Entity
{
    public class CidadaoUser : BaseEntity
    {
        public CidadaoUser(string nome, string senha, string login, string cPF)
        {
            Nome = nome;
            Senha = senha;
            Login = login;
            CPF = cPF;
        }

        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Login { get; private set; }
        public string CPF { get; private set; }
    }
}
