using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities
{
    public sealed class Municipio : BaseEntity
    {
        public string Nome { get; set; }
        public string Prefeito { get; set; }
        public IReadOnlyCollection<Instituicoes> Instituicoes { get; set; }
        public IReadOnlyCollection<Projetos> Projetos { get; set; }
    }
}
