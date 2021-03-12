using Flunt.Notifications;
using SGM.Cidadao.Application.Commands.Cidadao;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application
{
    public class CidadaoHandler : Notifiable, ICommandHandler<CadastrarCidadaoCommand>, ICommandHandler<DeletarCidadaoCommand>,
                                            ICommandHandler<EditarCidadaoCommand>
    {
        public ValueTask<ICommandResult> Handle(EditarCidadaoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(CadastrarCidadaoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(DeletarCidadaoCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
