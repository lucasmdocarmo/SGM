using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using SGM.Cidadao.Domain.Entities;

namespace SGM.Cidadao.Domain.Entities
{
    public sealed class Cidadao :BaseEntity
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public CPF CPF { get; private set; }
        public string Identidade { get; private set; }
        public bool Sexo { get; private set; }
        public string Email { get; private set; }
        public string Profissao { get; private set; }
        public string CodigoCidadao { get; private set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public Endereco Endereco { get; set; }
        public IReadOnlyCollection<Contribuicao>Contribuicao { get; set; }

    }
}
