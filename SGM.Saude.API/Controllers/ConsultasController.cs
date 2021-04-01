﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Saude.Application.Commands;
using SGM.Saude.Infra.Repositories.Contracts;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Saude.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ConsultasController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarConsultaCommand> _commandCadastrar;
        private readonly ICommandHandler<EditarConsultaCommand> _commandEditar;
        private readonly ICommandHandler<DeletarConsultaCommand> _commandDeletar;
        private readonly IConsultaRepository _repository;

        public ConsultasController(ICommandHandler<CadastrarConsultaCommand> commandCadastrar, ICommandHandler<EditarConsultaCommand> commandEditar, 
            ICommandHandler<DeletarConsultaCommand> commandDeletar, IConsultaRepository repository)
        {
            _commandCadastrar = commandCadastrar;
            _commandEditar = commandEditar;
            _commandDeletar = commandDeletar;
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItems()
        {
            var result = await _repository.GetAll().ConfigureAwait(true);
            if (result is null) { return NoContent(); }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoItem([Bind][FromRoute][Required] Guid id)
        {
            var result = await _repository.GetById(id).ConfigureAwait(true);
            if (result is null) { return NoContent(); }
            return Ok(result);
        }

        [HttpGet("Paciente/{cpf}")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoItem([FromRoute][Required] string cpf)
        {
            var result = await _repository.Search(x=>x.Paciente.CPF == cpf).ConfigureAwait(true);
            if (result is null) { return NoContent(); }
            var consultas = result.Where(x => x.DataConsulta > DateTime.Now).ToList();
            return Ok(consultas);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoItem([FromRoute][Bind] Guid id, [FromBody][Bind] EditarConsultaCommand command)
        {
            if (!ModelState.IsValid)
            {
                var notifications = new List<Notification>();
                foreach (var erro in ModelState.Where(a => a.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).ToList())
                {
                    notifications.Add(new Notification("invalidModel", erro.ErrorMessage));
                }
                return StatusCode(412, notifications.ToList());
            }
            command.Id = id;
            var result = await _commandEditar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity();
            }
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTodoItem([FromBody] CadastrarConsultaCommand command)
        {
            if (!ModelState.IsValid)
            {
                var notifications = new List<Notification>();
                foreach (var erro in ModelState.Where(a => a.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).ToList())
                {
                    notifications.Add(new Notification("invalidModel", erro.ErrorMessage));
                }
                return StatusCode(412, notifications.ToList());
            }

            var result = await _commandCadastrar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity(result.Messages);
            }
            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItem([FromRoute][Bind][Required] Guid id)
        {
            var command = new DeletarConsultaCommand() { Id = id };
            var result = await _commandDeletar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity();
            }
            return Ok(result.Result);
        }
    }
}
