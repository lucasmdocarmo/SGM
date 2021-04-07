using Flunt.Notifications;
using SGM.Manager.Application.Commands.Funcionario;
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
    public class FuncionarioHandler : Notifiable, ICommandHandler<CadastrarFuncionarioCommand>, ICommandHandler<DeletarFuncionarioCommand>,
                                            ICommandHandler<EditarFuncionarioCommand>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioHandler(IDepartamentoRepository departamentoRepository, IFuncionarioRepository funcionarioRepository)
        {
            _departamentoRepository = departamentoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarFuncionarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var entityDepartamento = await _departamentoRepository.GetById(command.DepartamentoId).ConfigureAwait(true);
            if(entityDepartamento is null)
            {
                return new CommandResult(false, "Departamento Não Encontrado.");
            }
            var entity = new Domain.Entities.Funcionario(command.Nome, command.Login, command.Senha,
                                                        command.TipoFuncionario, command.DepartamentoId,command.CPF);

            await _funcionarioRepository.Add(entity);
            var result = await _funcionarioRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarFuncionarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _funcionarioRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityDepartamento = await _departamentoRepository.GetById(command.DepartamentoId).ConfigureAwait(true);
            if (entityDepartamento is null)
            {
                return new CommandResult(false, "Departamento Não Encontrado.");
            }
            entity.EditarFuncionario(command.Nome,command.Login,command.Senha,command.TipoFuncionario,command.DepartamentoId);
            await _funcionarioRepository.Update(entity);
            var result = await _funcionarioRepository.SaveChanges().ConfigureAwait(true);

            if (result)
            {
                return new CommandResult(true);
            }

            return new CommandResult(false, "Houve um erro ao atualizar os registros.");
        }

        public async ValueTask<ICommandResult> Handle(DeletarFuncionarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _funcionarioRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _funcionarioRepository.Remove(command.Id);
                await _funcionarioRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }
            return new CommandResult(false, "Entidade Nao Encontrada.");
        }
    }
}
