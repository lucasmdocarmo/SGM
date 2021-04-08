using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Application.Commands.Funcionario
{
    public class EditarFuncionarioCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ETipoFuncionario TipoFuncionario { get; set; }
        public Guid DepartamentoId { get; set; }
        public Guid Id { get; set; }
        public bool Validate()
        {
            return true;
        }
    }
}
