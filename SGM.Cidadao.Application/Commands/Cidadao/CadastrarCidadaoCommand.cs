using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Cidadao
{
    public class CadastrarCidadaoCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string CPF_Estado { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
        public string CodigoCidadao { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
