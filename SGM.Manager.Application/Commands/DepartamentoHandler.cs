using Flunt.Notifications;
using SGM.Manager.Application.Commands.Departamento;
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
    public class DepartamentoHandler : Notifiable, ICommandHandler<CadastrarDepartamentoCommand>, ICommandHandler<DeletarDepartamentoCommand>,
                                            ICommandHandler<EditarDepartamentoCommand>
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoHandler(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarDepartamentoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Domain.Entities.Departamento(command.Nome, command.Codigo);

            await _departamentoRepository.Add(entity);
            var result = await _departamentoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarDepartamentoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _departamentoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _departamentoRepository.Remove(command.Id);
                await _departamentoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }
            return new CommandResult(false, "Entidade Nao Encontrada.");

        }

        public async ValueTask<ICommandResult> Handle(EditarDepartamentoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _departamentoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarDepartamento(command.Nome,command.Codigo);

            await _departamentoRepository.Update(entity);
            var result = await _departamentoRepository.SaveChanges().ConfigureAwait(true);
            if (result)
            {
                return new CommandResult(true);
            }

            return new CommandResult(false, "Houve um erro ao atualizar os registros.");
        }
    }
}
