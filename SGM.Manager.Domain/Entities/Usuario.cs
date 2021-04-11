using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public Usuario()
        {

        }
        public Usuario(string nome, string senha, string login, ETipoUsuario tipoUsuario, Guid departamentoId, string cPF)
        {
            Nome = nome;
            Senha = senha;
            Login = login;
            TipoUsuario = tipoUsuario;
            DepartamentoId = departamentoId;
            CPF = cPF;
        }

        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public Guid DepartamentoId { get; set; }
        public string CPF { get; set; }
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
