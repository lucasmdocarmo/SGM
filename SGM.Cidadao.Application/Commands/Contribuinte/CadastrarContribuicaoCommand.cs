using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Contribuinte
{
    public class CadastrarContribuicaoCommand : Notifiable, ICommand
    {
        public string CodigoGuiaContribuicao { get; set; }
        public string AnoFiscal { get; set; }
        public decimal TotalImpostos { get; set; }
        public DateTime Pagamento { get; set; }
        public Guid ImpostoId { get; set; }
        public Guid CidadaoId { get; set; }
        public Guid ContribuinteId { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
