using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Clinicas
{
    public sealed class Clinica : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public IReadOnlyCollection<Medicos> Medicos { get; set; }
    }
}
