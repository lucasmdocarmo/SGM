using SGM.Shared.Core.Contracts;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Domain.Entities
{
    public sealed class Paciente : BaseEntity, IAggregateRoot
    {
        internal Paciente() { }
        public Paciente(string nome, DateTime dataNascimento, string cPF, string identidade, bool sexo,
            string detalhesPaciente, string informacoesMedicas)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cPF;
            Identidade = identidade;
            Sexo = sexo;
            Status = ETipoStatusPaciente.Aguardando;
            DetalhesPaciente = detalhesPaciente;
            InformacoesMedicas = informacoesMedicas;
        }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public ETipoStatusPaciente Status { get; set; }
        public string DetalhesPaciente { get; set; }
        public string InformacoesMedicas { get; set; }
        public IReadOnlyCollection<Consultas> Consultas { get; set; }

        public void EditarPaciente(string nome, DateTime dataNascimento, string cPF, string identidade, bool sexo, ETipoStatusPaciente status,
            string detalhesPaciente, string informacoesMedicas)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cPF;
            Identidade = identidade;
            Sexo = sexo;
            Status = status;
            DetalhesPaciente = detalhesPaciente;
            InformacoesMedicas = informacoesMedicas;
        }
    }
}
