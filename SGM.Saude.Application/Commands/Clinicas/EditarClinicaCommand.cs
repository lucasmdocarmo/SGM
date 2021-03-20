using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands.Clinicas
{
    public class EditarClinicaCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
