using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities
{
    public sealed class Funcionario :BaseEntity
    {
        public Funcionario(string nome, string login, string senha, ETipoFuncionario tipoFuncionario, Guid departamentoId)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            TipoFuncionario = tipoFuncionario;
            DepartamentoId = departamentoId;
        }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public ETipoFuncionario TipoFuncionario { get; private set; }
        public Guid DepartamentoId { get; private set; }

        public Departamento Departamento { get; set; }

        public void EditarFuncionario(string nome, string login, string senha, ETipoFuncionario tipoFuncionario, Guid departamentoId)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            TipoFuncionario = tipoFuncionario;
            DepartamentoId = departamentoId;
        }
    }
}
