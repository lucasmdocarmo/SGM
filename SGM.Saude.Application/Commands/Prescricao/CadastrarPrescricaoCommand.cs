using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands.Prescricao
{
    public class CadastrarPrescricaoCommand : Notifiable, ICommand
    {
        public string Detalhes { get; set; }
        public bool Retornar { get; set; }
        public string Resumo { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime Validade { get; set; }
        public Guid ConsultaId { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
