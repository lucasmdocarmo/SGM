using Flunt.Validations;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities.Clinicas
{
    public sealed class Endereco :BaseEntity
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
            ClinicaId = clinicaId;

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

        public Clinica Clinica { get; set; }
        public Guid ClinicaId { get; set; }

        public IReadOnlyCollection<Medicos> Medicos { get; set; }
    }
}
