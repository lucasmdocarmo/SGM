using Flunt.Notifications;
using SGM.Manager.Application.Commands.Usuario;
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
    public class UsuarioHandler : Notifiable, ICommandHandler<CadastrarUsuarioCommand>, ICommandHandler<DeletarUsuarioCommand>,
                                            ICommandHandler<EditarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public UsuarioHandler(IUsuarioRepository usuarioRepository, IDepartamentoRepository departamentoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _departamentoRepository = departamentoRepository;
        }

        public async ValueTask<ICommandResult> Handle(CadastrarUsuarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entityDepartamento = await _departamentoRepository.GetById(command.DepartamentoId).ConfigureAwait(true);
            if (entityDepartamento is null)
            {
                return new CommandResult(false, "Departamento Não Encontrado.");
            }

            var entity = new Domain.Entities.Usuario(command.Nome, command.Senha, command.Login, command.TipoUsuario, command.DepartamentoId);

            await _usuarioRepository.Add(entity);
            var result = await _usuarioRepository.SaveChanges().ConfigureAwait(true);

            if (!result) { return new CommandResult(false); }

            return new CommandResult(true);
        }

        public async ValueTask<ICommandResult> Handle(EditarUsuarioCommand command)
        {

            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }
            var entity = await _usuarioRepository.GetById(command.Id);
            if (entity is null)
            {
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            var entityDepartamento = await _departamentoRepository.GetById(command.DepartamentoId).ConfigureAwait(true);
            if (entityDepartamento is null)
            {
                return new CommandResult(false, "Departamento Não Encontrado.");
            }

            entity.EditarUsuario(command.Nome, command.Senha, command.Login, command.TipoUsuario, command.DepartamentoId);

            await _usuarioRepository.Update(entity);
            var result = await _usuarioRepository.SaveChanges().ConfigureAwait(true);
            if (result)
            {
                return new CommandResult(true);
            }

            return new CommandResult(false, "Houve um erro ao atualizar os registros.");
        }

        public async ValueTask<ICommandResult> Handle(DeletarUsuarioCommand command)
        {
            if (!command.Validate())
            {
                AddNotifications(command);
                return new CommandResult(false, base.Notifications);
            }

            var result = await _usuarioRepository.GetById(command.Id).ConfigureAwait(true);
            if (result != null)
            {
                await _usuarioRepository.Remove(command.Id);
                await _usuarioRepository.SaveChanges().ConfigureAwait(true);
                return new CommandResult(true);
            }
            return new CommandResult(false, "Entidade Nao Encontrada.");
        }
    }
}
