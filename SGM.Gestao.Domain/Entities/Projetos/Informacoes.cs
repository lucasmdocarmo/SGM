using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities
{
    public sealed class Informacoes :BaseEntity
    {
        public string Descricao { get; set; }
        public Projetos Projeto { get; set; }
        public Guid ProjetoId { get; set; }
    }
}
