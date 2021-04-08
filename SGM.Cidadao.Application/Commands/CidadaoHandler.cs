using Flunt.Notifications;
using SGM.Cidadao.Application.Commands;
using SGM.Cidadao.Application.Commands.Cidadao;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Shared.Core.Commands;
using SGM.Shared.Core.Commands.Handler;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SGM.Cidadao.Domain.Entities;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Exceptions;
using SGM.Shared.Core.Excepctions;
using System.Net.Http;
using Newtonsoft.Json;
using SGM.Shared.Core.Entity;

namespace SGM.Cidadao.Application
{
    public class CidadaoHandler : Notifiable, ICommandHandler<CadastrarCidadaoCommand>, ICommandHandler<DeletarCidadaoCommand>,
                                            ICommandHandler<EditarCidadaoCommand>
    {
        private readonly ICidadaoRepository _cidadaoRepository;
        public static HttpClient _Client;
        public CidadaoHandler(ICidadaoRepository cidadaoRepository)
        {
            _cidadaoRepository = cidadaoRepository;
            _Client = new HttpClient();
        }

        public async ValueTask<ICommandResult> Handle(EditarCidadaoCommand command)
        {
            try
            {
                if (!command.Validate())
                {
                    AddNotifications(command);
                    return new CommandResult(false, base.Notifications);
                }
                var entity = await _cidadaoRepository.GetById(command.Id);
                if (entity is null)
                {
                    return new CommandResult(false, "Entidade Nao Encontrada.");
                }

                entity.EditarCidadao(command.Nome, command.DataNascimento,command.CPF,
                    command.Identidade, command.Sexo, command.Email, command.Profissao, command.Telefone, command.Celular);

                await _cidadaoRepository.Update(entity);
                var result = await _cidadaoRepository.SaveChanges().ConfigureAwait(true);
                if (result)
                {
                    return new CommandResult(true);
                }

                return new CommandResult(false, "Houve um erro ao atualizar os registros.");
            }
            catch (InternalAppException ex)
            {
                throw new InternalAppException(ex.Message);
            }
        }

        public async ValueTask<ICommandResult> Handle(CadastrarCidadaoCommand command)
        {
            try
            {
                if (!command.Validate())
                {
                    AddNotifications(command);
                    return new CommandResult(false, base.Notifications);
                }

                var entity = new Domain.Entities.Cidadao(command.Nome, command.DataNascimento, command.CPF, command.Identidade,
                    command.Sexo, command.Email, command.Profissao, command.Telefone, command.Celular);

                await _cidadaoRepository.Add(entity);
                var result = await _cidadaoRepository.SaveChanges().ConfigureAwait(true);

                ReplicarUsuario(command);
                ReplicarPaciente(command);

                if (!result) { return new CommandResult(false); }

                return new CommandResult(true);
            }
            catch(InternalAppException ex)
            {
                throw new InternalAppException(ex.Message);
            }

        }
        private void ReplicarUsuario(CadastrarCidadaoCommand command)
        {
            var cidadaoUsuario = new CidadaoUser(command.Nome, command.Senha, command.Email,command.CPF);

            var json = JsonConvert.SerializeObject(cidadaoUsuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            _Client.PostAsync("https://localhost:44393/api/v1/Usuario/Cidadao", data).ConfigureAwait(true);
        }
        public void ReplicarPaciente(CadastrarCidadaoCommand command)
        {
            var cidadaoUsuario = new CadastrarPaciente(command.Nome, command.DataNascimento, command.CPF, command.Identidade, command.Sexo,
                                    ETipoStatusPaciente.Aguardando, "Paciente Criado com Sucesso", " ");

            var json = JsonConvert.SerializeObject(cidadaoUsuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            _Client.PostAsync("https://localhost:44397/api/v1/Paciente", data).ConfigureAwait(true);
        }

        public async ValueTask<ICommandResult> Handle(DeletarCidadaoCommand command)
        {
            try
            {
                if (!command.Validate())
                {
                    AddNotifications(command);
                    return new CommandResult(false, base.Notifications);
                }

                var result = await _cidadaoRepository.GetById(command.Id).ConfigureAwait(true);
                if (result != null)
                {
                    await _cidadaoRepository.Remove(command.Id);
                    await _cidadaoRepository.SaveChanges().ConfigureAwait(true);
                    return new CommandResult(true);
                }
                return new CommandResult(false, "Entidade Nao Encontrada.");
            }
            catch (InternalAppException ex)
            {
                throw new InternalAppException(ex.Message);
            }
        }
    }
}
