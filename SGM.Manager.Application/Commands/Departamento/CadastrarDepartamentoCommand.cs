using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Departamento
{
    public class CadastrarDepartamentoCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
