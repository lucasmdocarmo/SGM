using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities
{
    public sealed class Departamento : BaseEntity
    {
        public Departamento(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        public string Nome { get; private set; }
        public string Codigo { get; private set; }

        public IReadOnlyCollection<Funcionario> Funcionario { get; set; }
        public IReadOnlyCollection<Usuario> Usuario { get; set; }

        public void EditarDepartamento(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
    }
}
