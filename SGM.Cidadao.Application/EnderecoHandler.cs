using Flunt.Notifications;
using SGM.Cidadao.Application.Commands.Endereco;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application
{
    public class EnderecoHandler : Notifiable, ICommandHandler<CadastrarEnderecoCommand>, ICommandHandler<DeletarEnderecoCommand>,
                                               ICommandHandler<EditarEnderecoCommand>
    {
        public ValueTask<ICommandResult> Handle(CadastrarEnderecoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(DeletarEnderecoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(EditarEnderecoCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
