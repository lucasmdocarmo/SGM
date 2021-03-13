using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities
{
    public sealed class Usuario : BaseEntity
    {
        public Usuario(string nome, string senha, string login, ETipoUsuario tipoUsuario, Guid departamentoId)
        {
            Nome = nome;
            Senha = senha;
            Login = login;
            TipoUsuario = tipoUsuario;
            DepartamentoId = departamentoId;
        }

        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Login { get; private set; }
        public ETipoUsuario TipoUsuario { get; private set; }
        public Guid DepartamentoId { get; private set; }

        public Departamento Departamento { get; set; }

        public void EditarUsuario(string nome, string senha, string login, ETipoUsuario tipoUsuario, Guid departamentoId)
        {
            Nome = nome;
            Senha = senha;
            Login = login;
            TipoUsuario = tipoUsuario;
            DepartamentoId = departamentoId;
        }

    }
}
