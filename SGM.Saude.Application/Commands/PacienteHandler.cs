using Flunt.Notifications;
using SGM.Saude.Application.Commands.Paciente;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SGM.Saude.Domain.Entities;
using SGM.Saude.Application.Commands.Pacientes;

namespace SGM.Saude.Application.Commands
{
    public class PacienteHandler : Notifiable, ICommandHandler<CadastrarPacienteCommand>, ICommandHandler<DeletarPacienteCommand>,
                                            ICommandHandler<EditarPacienteCommand>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarPacienteCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Paciente(command.Nome, command.DataNascimento, command.CPF, command.Identidade, command.Sexo, 
                command.Status, command.DetalhesPaciente, command.InformacoesMedicas);

            await _pacienteRepository.Add(entity);
            var result = await _pacienteRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarPacienteCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _pacienteRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _pacienteRepository.Remove(command.Id);
                await _pacienteRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(EditarPacienteCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _pacienteRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarPaciente(command.Nome,command.DataNascimento,command.CPF,command.Identidade,command.Sexo,command.Status,command.DetalhesPaciente,
                command.InformacoesMedicas);

            await _pacienteRepository.Update(entity);
            var result = await _pacienteRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
