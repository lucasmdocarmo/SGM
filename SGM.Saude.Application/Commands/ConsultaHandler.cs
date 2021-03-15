using Flunt.Notifications;
using SGM.Saude.Domain.Entities;
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
    public class ConsultaHandler : Notifiable, ICommandHandler<CadastrarConsultaCommand>, ICommandHandler<DeletarConsultaCommand>,
                                            ICommandHandler<EditarConsultaCommand>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicosRepository _medicoRepository;

        public ConsultaHandler(IConsultaRepository consultaRepository, IPacienteRepository pacienteRepository, IMedicosRepository medicoRepository)
        {
            _consultaRepository = consultaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public async ValueTask<ICommandResult> Handle(DeletarConsultaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _consultaRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _consultaRepository.Remove(command.Id);
                await _consultaRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(CadastrarConsultaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entityMedico = await _medicoRepository.GetById(command.MedicoId);
            if (entityMedico is null)
            {
                return new CommandResult(false, "Medico Nao Encontrada.");
            }
            var entityPaciente = await _pacienteRepository.GetById(command.PacienteId);
            if (entityPaciente is null)
            {
                return new CommandResult(false, "Paciente Nao Encontrada.");
            }
            var entity = new Consultas(command.Especialidade, command.Descricao, command.InformacoesMedicas, command.DataConsulta,
                                command.Confirmada, command.PacienteId, command.MedicoId);

            await _consultaRepository.Add(entity);
            var result = await _consultaRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarConsultaCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _consultaRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityMedico = await _medicoRepository.GetById(command.MedicoId);
            if (entityMedico is null)
            {
                return new CommandResult(false, "Medico Nao Encontrada.");
            }
            var entityPaciente = await _pacienteRepository.GetById(command.PacienteId);
            if (entityPaciente is null)
            {
                return new CommandResult(false, "Paciente Nao Encontrada.");
            }

            entity.EditarConsulta(command.Especialidade, command.Descricao, command.InformacoesMedicas, command.DataConsulta,
                                command.Confirmada, command.PacienteId, command.MedicoId);

            await _consultaRepository.Update(entity);
            var result = await _consultaRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
