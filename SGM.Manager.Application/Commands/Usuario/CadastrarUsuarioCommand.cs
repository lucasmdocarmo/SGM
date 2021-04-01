using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Usuario
{
    public class CadastrarUsuarioCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public Guid DepartamentoId { get; set; }
        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
