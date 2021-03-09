using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities
{
    public sealed class Departamento : BaseEntity
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public IReadOnlyCollection<Funcionario> Funcionario { get; set; }
        public IReadOnlyCollection<Usuario> Usuario { get; set; }

    }
}
