using Flunt.Notifications;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Application.Commands.Contribuinte;
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
    public class ContribuinteHandler : Notifiable, ICommandHandler<CadastrarContribuicaoCommand>,
                                                 ICommandHandler<DeletarContribuicaoCommand>, ICommandHandler<EditarContribuicaoCommand>

    {
        private readonly IContribuicaoRepository _contribuicaoRepository;
        private readonly ICidadaoRepository _cidadaoRepository;
        private readonly IImpostosRepository _impostoRepository;

        public ContribuinteHandler(IContribuicaoRepository contribuicaoRepository, ICidadaoRepository cidadaoRepository, IImpostosRepository impostoRepository)
        {
            _contribuicaoRepository = contribuicaoRepository;
            _cidadaoRepository = cidadaoRepository;
            _impostoRepository = impostoRepository;
        }

        public async ValueTask<ICommandResult> Handle(DeletarContribuicaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _contribuicaoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _contribuicaoRepository.Remove(command.Id);
                await _contribuicaoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }
            
            return new CommandResult(false, "Entidade Nao Encontrada.");

        }

        public async ValueTask<ICommandResult> Handle(CadastrarContribuicaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entity = new Contribuicao(command.CodigoGuiaContribuicao,command.AnoFiscal,command.TotalImpostos,command.Pagamento,
                command.CidadaoId,command.ImpostoId,command.ContribuinteId);

            await _contribuicaoRepository.Add(entity);
            var result = await _contribuicaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarContribuicaoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _contribuicaoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityCidadao = await _cidadaoRepository.GetById(command.CidadaoId);
            if (entityCidadao is null)
            {
                return new CommandResult(false, "Cidadao Não Encontrado.");
            }
            var entityImposto = await _impostoRepository.GetById(command.ImpostoId);
            if (entityImposto is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarContribuinte(command.CodigoGuiaContribuicao,command.AnoFiscal,command.TotalImpostos,command.Pagamento,
                command.CidadaoId,command.ImpostoId,command.ContribuinteId);

            await _contribuicaoRepository.Update(entity);
            var result = await _contribuicaoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);

        }
    }
}
