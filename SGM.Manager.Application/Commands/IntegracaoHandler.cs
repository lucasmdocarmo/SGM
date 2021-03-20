using Flunt.Notifications;
using SGM.Manager.Application.Commands.Integracoes;
using SGM.Manager.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Manager.Application.Commands
{
    public class IntegracaoHandler : Notifiable, ICommandHandler<CadastrarIntegracaoCommand>, ICommandHandler<DeletarIntegracoesCommand>,
                                            ICommandHandler<EditarIntegracoesCommand>
    {
        private readonly IIntegracaoRepository _integracaoRepository;

        public IntegracaoHandler(IIntegracaoRepository integracaoRepository)
        {
            _integracaoRepository = integracaoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarIntegracaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Domain.Entities.Integration.AppIntegration(command.Sistema);

            await _integracaoRepository.Add(entity);
            var result = await _integracaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }
            var integracao = await _integracaoRepository.GetById(entity.Id).ConfigureAwait(true);
            return new CommandResult(true, integracao);
        }

        public async ValueTask<ICommandResult> Handle(DeletarIntegracoesCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _integracaoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _integracaoRepository.Remove(command.Id);
                await _integracaoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }
            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(EditarIntegracoesCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _integracaoRepository.GetById(command.Id).ConfigureAwait(true);

            result.Sistema = command.Sistema;

            await _integracaoRepository.Update(result);
            await _integracaoRepository.SaveChanges().ConfigureAwait(true);
            return new CommandResult(true);
        }
    }
}
