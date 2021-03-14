using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands.Prescricao
{
    public class DeletarPrescricaoCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
