using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Cidadao
{
    public class CadastrarPaciente
    {
        public CadastrarPaciente(string nome, DateTime dataNascimento, string cPF, string identidade, bool sexo, ETipoStatusPaciente status, string detalhesPaciente, string informacoesMedicas)
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

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public ETipoStatusPaciente Status { get; set; }
        public string DetalhesPaciente { get; set; }
        public string InformacoesMedicas { get; set; }
    }
}
