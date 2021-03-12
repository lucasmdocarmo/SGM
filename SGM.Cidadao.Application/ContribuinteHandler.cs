using Flunt.Notifications;
using SGM.Cidadao.Application.Commands.Contribuinte;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application
{
    public class ContribuinteHandler : Notifiable, ICommandHandler<CadastrarContribuicaoCommand>,
                                                 ICommandHandler<DeletarContribuicaoCommand>, ICommandHandler<EditarContribuicaoCommand>

    {
        public ValueTask<ICommandResult> Handle(DeletarContribuicaoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(CadastrarContribuicaoCommand command)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICommandResult> Handle(EditarContribuicaoCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
