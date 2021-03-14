using Flunt.Notifications;
using SGM.Saude.Application.Commands.Endereco;
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
    public class EnderecoHandler : Notifiable, ICommandHandler<CadastrarEnderecoCommand>, ICommandHandler<DeletarEnderecoCommand>,
                                            ICommandHandler<EditarEnderecoCommand>
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMedicosRepository _medicoRepository;

        public EnderecoHandler(IEnderecoRepository enderecoRepository, IMedicosRepository medicoRepository)
        {
            _enderecoRepository = enderecoRepository;
            _medicoRepository = medicoRepository;
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

        public async ValueTask<ICommandResult> Handle(CadastrarEnderecoCommand command)
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

            var entity = new Endereco(command.CEP, command.Logradouro, command.Numero, command.Complemento, command.Cidade,
                                command.Estado, command.MedicoId);

            await _enderecoRepository.Add(entity);
            var result = await _enderecoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
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
            var entityMedico = await _medicoRepository.GetById(command.MedicoId);
            if (entityMedico is null)
            {
                return new CommandResult(false, "Medico Nao Encontrada.");
            }

            entity.EditarEndereco(command.CEP, command.Logradouro, command.Numero, command.Complemento, command.Cidade,
                                command.Estado, command.MedicoId);

            await _enderecoRepository.Update(entity);
            var result = await _enderecoRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false, "Houve um erro ao atualizar os registros."); }
            return new CommandResult(true);
        }
    }
}
