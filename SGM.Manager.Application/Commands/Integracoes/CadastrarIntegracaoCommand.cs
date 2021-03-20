using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Integracoes
{
    public class CadastrarIntegracaoCommand : Notifiable, ICommand
    {
        public string Sistema { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
