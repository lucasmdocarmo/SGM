using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Clinicas
{
    public sealed class Clinica : BaseEntity, IAggregateRoot
    {
        public Clinica(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
        }

        public string Nome { get; private set; }
        public string Telefone { get; private set; }

        public IReadOnlyCollection<Medicos> Medicos { get; set; }

        public void EditarClinica(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
        }
    }
}
