using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Cidadao
{
    public class DeletarCidadaoCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string CodigoCidadao { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
