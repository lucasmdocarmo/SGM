using Flunt.Notifications;
using SGM.Cidadao.Application.Commands.StatusContribuinte;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Cidadao.Application.Commands
{
    public class StatusContribuinteHandler : Notifiable, ICommandHandler<CadastrarStatusCommand>, ICommandHandler<DeletarStatusCommand>,
                                               ICommandHandler<EditarStatusCommand>
    {
        private readonly IStatusContribuicaoRepository _statusContribuicaoRepository;
        private readonly IContribuicaoRepository _contribuicaoRepository;

        public StatusContribuinteHandler(IStatusContribuicaoRepository statusContribuicaoRepository, IContribuicaoRepository impostoRepository)
        {
            _statusContribuicaoRepository = statusContribuicaoRepository;
            _contribuicaoRepository = impostoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarStatusCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new StatusContribuicao(command.CodigoGuiaContribuicao, command.Status, command.RegistroInicial, command.RegistroFinal, false);

            await _statusContribuicaoRepository.Add(entity);
            var result = await _statusContribuicaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarStatusCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _statusContribuicaoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarStatus(command.CodigoGuiaContribuicao, command.Status, command.RegistroInicial, command.RegistroFinal, command.Finalizado);

            await _statusContribuicaoRepository.Update(entity);
            var result = await _statusContribuicaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarStatusCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _statusContribuicaoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _statusContribuicaoRepository.Remove(command.Id);
                await _statusContribuicaoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }
    }
}
