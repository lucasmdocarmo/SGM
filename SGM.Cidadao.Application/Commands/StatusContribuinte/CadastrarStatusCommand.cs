using Flunt.Notifications;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.StatusContribuinte
{
    public class CadastrarStatusCommand : Notifiable, ICommand
    {
        public string CodigoGuiaContribuicao { get; set; }
        public ETipoStatusContribuinte Status { get; set; }
        public DateTime RegistroInicial { get; set; }
        public DateTime RegistroFinal { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
