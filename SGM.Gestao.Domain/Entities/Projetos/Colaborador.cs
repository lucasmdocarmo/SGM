using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities
{
    public sealed class Colaborador :BaseEntity
    {
        public string Nome { get; set; }
        public ETipoFuncao TipoFuncao { get; set; }
        public ETipoColaborador TipoColaborador { get; set; }
        public CPF CPF { get; set; }
    }
}
