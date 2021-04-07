using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Usuario
{
    public class CadastrarUsuarioCidadoCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string CPF { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
