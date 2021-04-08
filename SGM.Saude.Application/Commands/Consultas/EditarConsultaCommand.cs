using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands
{
    public class EditarConsultaCommand : Notifiable, ICommand
    {
        public string Especialidade { get; private set; }
        public string Descricao { get; private set; }
        public string InformacoesMedicas { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public bool Confirmada { get; set; }
        public Guid PacienteId { get; private set; }
        public Guid MedicoId { get; private set; }
        public Guid Id { get; set; }
        public bool Validate()
        {
            return true;
        }
    }
}
