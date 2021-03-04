using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Auth.Domain.Entities
{
    public sealed class Funcionario :BaseEntity
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ETipoFuncionario TipoFuncionario { get; set; }

        public Departamento Departamento { get; set; }
        public Guid DepartamentoId { get; set; }
    }
}
