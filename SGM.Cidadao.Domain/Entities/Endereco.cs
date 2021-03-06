using Flunt.Validations;
using SGM.Cidadao.Domain.Entitiy;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Domain.Entities
{
    public sealed class Endereco : BaseEntity
    {
        internal Endereco() { }
        public Endereco(string CEP, string logradouro, string numero, string complemento, string cidade, string estado, Guid clinicaId)
        {
            this.CEP = CEP;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;

            AddNotifications(new Contract()
                .Requires()
                .IsNotEmpty(base.Id, this.CEP, "CEP é Obrigatorio.")
            );

        }

        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Cidadaos Cidadao { get; private set; }
        public Guid CidadaoId { get; private set; }
    }
}
