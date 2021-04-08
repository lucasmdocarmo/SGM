using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands
{
    public class EditarMedicoCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public bool AtendeSegunda { get; set; }
        public bool AtendeTerca { get; set; }
        public bool AtendeQuarta { get; set; }
        public bool AtendeQuinta { get; set; }
        public bool AtendeSexta { get; set; }
        public bool AtendeSabado { get; set; }
        public bool AtendeDomingo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public decimal ValorHora { get; set; }
        public Guid ClinicaId { get; set; }
        public Guid Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
