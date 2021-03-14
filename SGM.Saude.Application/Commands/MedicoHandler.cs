using Flunt.Notifications;
using SGM.Saude.Application.Commands.Medicos;
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
    public class MedicoHandler : Notifiable, ICommandHandler<CadastrarMedicoCommand>, ICommandHandler<DeletarMedicoCommand>,
                                            ICommandHandler<EditarMedicoCommand>
    {
        private readonly IMedicosRepository _medicoRepository;
        private readonly IClinicaRepository _clinicaRepository;
        public MedicoHandler(IMedicosRepository medicoRepository, IClinicaRepository clinicaRepository)
        {
            _medicoRepository = medicoRepository;
            _clinicaRepository = clinicaRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarMedicoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
           
            var entity = new Medicos(command.Nome,command.Profissao,command.CRM,command.Especialidade,command.HoraInicio,command.HoraFim,
                command.Ativo,command.Email,command.Telefone,command.ValorHora,command.ClinicaId,command.AtendeSegunda,command.AtendeTerca,
                command.AtendeQuarta, command.AtendeQuinta, command.AtendeSexta, command.AtendeSabado);

            await _medicoRepository.Add(entity);
            var result = await _medicoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarMedicoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _medicoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _medicoRepository.Remove(command.Id);
                await _medicoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(EditarMedicoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _medicoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityClinica = await _clinicaRepository.GetById(command.ClinicaId);
            if (entityClinica is null)
            {
                return new CommandResult(false, "Clinica Nao Encontrada.");
            }

            entity.EditarMedico(command.Nome,command.Profissao,command.CRM,command.Especialidade,command.HoraInicio,command.HoraFim,
                command.Ativo,command.Email,command.Telefone,command.ValorHora,command.AtendeSegunda,command.AtendeTerca,
                command.AtendeQuarta,command.AtendeQuinta,command.AtendeSexta,command.AtendeSabado);

            await _medicoRepository.Update(entity);
            var result = await _medicoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
