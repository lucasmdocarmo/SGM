using Flunt.Notifications;
using SGM.Saude.Application.Commands.Prescricao;
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
    public class PrescricaoHandler : Notifiable, ICommandHandler<CadastrarPrescricaoCommand>, ICommandHandler<DeletarPrescricaoCommand>,
                                            ICommandHandler<EditarPrescricaoCommand>
    {
        private readonly IPrescricaoRepository _prescricaoRepository;
        private readonly IConsultaRepository _consultaRepository;
        public PrescricaoHandler(IPrescricaoRepository prescricaoRepository, IConsultaRepository consultaRepository)
        {
            _prescricaoRepository = prescricaoRepository;
            _consultaRepository = consultaRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarPrescricaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entityConsulta = await _consultaRepository.GetById(command.ConsultaId);
            if (entityConsulta is null)
            {
                return new CommandResult(false, "Consulta Nao Encontrada.");
            }
            var entity = new Domain.Entities.Consulta.Prescricao(command.Detalhes, command.Retornar, command.Resumo, command.DataInicial, 
                command.Validade, command.ConsultaId);

            await _prescricaoRepository.Add(entity);
            var result = await _prescricaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarPrescricaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _prescricaoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _prescricaoRepository.Remove(command.Id);
                await _prescricaoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(EditarPrescricaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _prescricaoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityConsulta = await _consultaRepository.GetById(command.ConsutaId);
            if (entityConsulta is null)
            {
                return new CommandResult(false, "Consulta Nao Encontrada.");
            }

            entity.EditarPrescricao(command.Detalhes,command.Retornar,command.Resumo,command.DataInicial,command.Validade,command.ConsutaId);

            await _prescricaoRepository.Update(entity);
            var result = await _prescricaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
