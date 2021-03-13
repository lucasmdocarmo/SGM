using Flunt.Notifications;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Application.Commands.Impostos;
using SGM.Cidadao.Domain.Entities.Contribuinte;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
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
        private readonly IImpostosRepository _impostoRepository;

        public ImpostosHandler(IImpostosRepository impostoRepository)
        {
            _impostoRepository = impostoRepository;
        }

        public async ValueTask<ICommandResult> Handle(EditarImpostosCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _impostoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarImpostos(command.Tributo,command.TipoImposto,command.DataFinal,command.AnoFiscal);

            await _impostoRepository.Update(entity);
            var result = await _impostoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);

        }

        public async ValueTask<ICommandResult> Handle(CadastrarImpostosCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Impostos(command.Tributo, command.TipoImposto, command.DataFinal, command.AnoFiscal);

            await _impostoRepository.Add(entity);
            var result = await _impostoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarImpostosCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _impostoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _impostoRepository.Remove(command.Id);
                await _impostoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }
    }
}
