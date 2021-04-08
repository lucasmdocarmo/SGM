using Flunt.Notifications;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.StatusContribuinte
{
    public class EditarStatusCommand : Notifiable, ICommand
    {
        public string CodigoGuiaContribuicao { get; set; }
        public ETipoStatusContribuinte Status { get; set; }
        public DateTime RegistroInicial { get; set; }
        public DateTime RegistroFinal { get; set; }
        public bool Finalizado { get; set; }
        public Guid Id { get; set; }
        public bool Validate()
        {
            return true;
        }
    }
}
