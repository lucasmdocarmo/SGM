using Flunt.Notifications;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Application.Commands.Endereco;
using SGM.Cidadao.Domain.Entities;
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
    public class EnderecoHandler : Notifiable, ICommandHandler<CadastrarEnderecoCommand>, ICommandHandler<DeletarEnderecoCommand>,
                                               ICommandHandler<EditarEnderecoCommand>
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ICidadaoRepository _cidadaoRepository;
        public EnderecoHandler(IEnderecoRepository enderecoRepository, ICidadaoRepository cidadaoRepository)
        {
            _enderecoRepository = enderecoRepository;
            _cidadaoRepository = cidadaoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarEnderecoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var clientEntity = await _cidadaoRepository.GetById(command.CidadaoId).ConfigureAwait(true);
            if(clientEntity is null)
            {
                return new CommandResult(false, "Cliente Não Encontrado.");
            }
            var entity = new Endereco(command.CEP, command.Logradouro, command.Numero, command.Complemento, command.Cidade,
                                      command.Estado, command.CidadaoId);

            await _enderecoRepository.Add(entity);
            var result = await _enderecoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarEnderecoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _enderecoRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _enderecoRepository.Remove(command.Id);
                await _enderecoRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }

            return new CommandResult(false, "Entidade Nao Encontrada.");
        }

        public async ValueTask<ICommandResult> Handle(EditarEnderecoCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _enderecoRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }

            entity.EditarEndereco(command.CEP, command.Logradouro, command.Numero, command.Complemento, command.Cidade,
                                      command.Estado, command.CidadaoId);

            await _enderecoRepository.Update(entity);
            var result = await _enderecoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
