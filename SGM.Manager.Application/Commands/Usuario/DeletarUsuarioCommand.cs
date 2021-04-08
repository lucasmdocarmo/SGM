using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Usuario
{
    public class DeletarUsuarioCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
