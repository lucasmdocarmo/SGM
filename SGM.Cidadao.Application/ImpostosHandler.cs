using Flunt.Notifications;
using SGM.Cidadao.Application.Commands.Impostos;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application
{
    public class ImpostosHandler : Notifiable, ICommandHandler<CadastrarImpostosCommand>, ICommandHandler<DeletarImpostosCommand>,
                                               ICommandHandler<EditarImpostosCommand>
    {
        public ValueTask<ICommandResult> Handle(EditarImpostosCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(CadastrarImpostosCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(DeletarImpostosCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
