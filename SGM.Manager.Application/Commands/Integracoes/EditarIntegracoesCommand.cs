using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Integracoes
{
    public class EditarIntegracoesCommand : Notifiable, ICommand
    {
        public string Sistema { get; set; }
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
