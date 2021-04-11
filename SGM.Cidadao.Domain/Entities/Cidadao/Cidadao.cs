using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Shared.Core.Entity;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using SGM.Cidadao.Domain.Entities;
using SGM.Shared.Core.Contracts;

namespace SGM.Cidadao.Domain.Entities
{
    public class Cidadao :BaseEntity , IAggregateRoot
    {
        public Cidadao() { }
        public Cidadao(string nome, DateTime dataNascimento, string cPF, string identidade, bool sexo, string email, 
            string profissao, string telefone, string celular)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cPF;
            Identidade = identidade;
            Sexo = sexo;
            Email = email;
            Profissao = profissao;
            CodigoCidadao = CodigoGenerator.GenerateCodigoCidadao();
            Telefone = telefone;
            Celular = celular;
        }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
        public string CodigoCidadao { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public Endereco Endereco { get; set; }
        public IReadOnlyCollection<Contribuicao>Contribuicao { get; set; }

        public void EditarCidadao(string nome, DateTime dataNascimento, string CPF, string identidade, bool sexo, string email,
            string profissao, string telefone, string celular)
        {
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.CPF = CPF;
            this.Identidade = identidade;
            this.Sexo = sexo;
            this.Email = email;
            this.Profissao = profissao;
            this.Telefone = telefone;
            this.Celular = celular;
        }

    }
    internal static class CodigoGenerator
    {
        private static readonly Random _random = new Random();
        public static string GenerateCodigoCidadao()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return "SGM" + _random.Next(0, 999999) + _random.Next(0, chars.Length - 1);
        }
    }
}
