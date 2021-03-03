using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Gestao.Domain.Entities
{
    public sealed class Projetos :BaseEntity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string CodigoProjeto { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public DateTime DataInicio { get; private set; }
        public ETipoStatusProjeto TipoStatus { get; private set; }
        public ETipoCategoriaProjeto TipooCategoria { get; private set; }
        public decimal Custo { get; private set; }

        public Guid MunicioId { get; private set; }
        public Municipio Municipio { get; private set; }
        public IReadOnlyCollection<Colaborador> Colaboradores { get; private set; }
        public IReadOnlyCollection<Informacoes> Informacoes { get; set; }
    }
}
