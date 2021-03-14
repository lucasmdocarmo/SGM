using Flunt.Notifications;
using SGM.Saude.Application.Commands.Clinicas;
using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Saude.Application.Commands
{
    public class ClinicaHandler : Notifiable, ICommandHandler<CadastrarClinicaCommand>, ICommandHandler<DeletarClinicaCommand>,
                                            ICommandHandler<EditarClinicaCommand>
    {
        private readonly IClinicaRepository _clinicaRepository;

        public ClinicaHandler(IClinicaRepository clinicaRepository)
        {
            _clinicaRepository = clinicaRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarClinicaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
           
            var entity = new Clinica(command.Nome, command.Telefone);
            await _clinicaRepository.Add(entity);
            var result = await _clinicaRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarClinicaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _clinicaRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarClinica(command.Nome, command.Telefone);

            await _clinicaRepository.Update(entity);
            var result = await _clinicaRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarClinicaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _clinicaRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _clinicaRepository.Remove(command.Id);
                await _clinicaRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }
    }
}
