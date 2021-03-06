using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities.Usuarios
{
    public sealed class Funcionarios :BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool Ativo { get; set; }
        public ETipoFuncao TipoFuncao { get; set; }
        public CPF CPF { get; private set; }
        public Instituicoes Instituicao { get; set; }
        public Guid InstituicaoId { get; set; }
    }
}
