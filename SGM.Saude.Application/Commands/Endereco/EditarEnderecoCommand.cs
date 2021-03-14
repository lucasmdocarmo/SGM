using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Saude.Application.Commands
{
    public class EditarEnderecoCommand : Notifiable, ICommand
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid MedicoId { get; set; }
        public Guid Id { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
