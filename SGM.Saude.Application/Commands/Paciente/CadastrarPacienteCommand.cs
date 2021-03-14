using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands.Pacientes
{
    public class CadastrarPacienteCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public CPF CPF { get; set; }
        public string Identidade { get; set; }
        public bool Sexo { get; set; }
        public ETipoStatusPaciente Status { get; set; }
        public string DetalhesPaciente { get; set; }
        public string InformacoesMedicas { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
