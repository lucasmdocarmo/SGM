using Flunt.Notifications;
using SGM.Cidadao.Domain.ValueObjects;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Impostos
{
    public class EditarImpostosCommand : Notifiable, ICommand
    {
        public decimal Tributo { get; set; }
        public ETipoImposto TipoImposto { get; set; }
        public DateTime DataFinal { get; set; }
        public string AnoFiscal { get; set; }
        public Guid Id { get; set; }
        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
