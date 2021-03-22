using Flunt.Notifications;
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
    public class MedicosController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarMedicoCommand> _commandCadastrar;
        private readonly ICommandHandler<EditarMedicoCommand> _commandEditar;
        private readonly ICommandHandler<DeletarMedicoCommand> _commandDeletar;
        private readonly IMedicosRepository _repository;

        public MedicosController(ICommandHandler<CadastrarMedicoCommand> commandCadastrar, ICommandHandler<EditarMedicoCommand> commandEditar, 
            ICommandHandler<DeletarMedicoCommand> commandDeletar, IMedicosRepository repository)
        {
            _commandCadastrar = commandCadastrar;
            _commandEditar = commandEditar;
            _commandDeletar = commandDeletar;
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Clinica, Administrador")]
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
        [Authorize(Roles = "Clinica, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItem([Bind][FromRoute][Required] Guid id)
        {
            var result = await _repository.GetById(id).ConfigureAwait(true);
            if (result is null) { return NoContent(); }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Clinica, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem([FromRoute][Bind] Guid id, [FromBody][Bind] EditarMedicoCommand command)
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
        [Authorize(Roles = "Clinica, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody] CadastrarMedicoCommand command)
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
        [Authorize(Roles = "Clinica, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem([FromRoute][Bind][Required] Guid id)
        {
            var command = new DeletarMedicoCommand() { Id = id };
            var result = await _commandDeletar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity();
            }
            return Ok(result.Result);
        }
    }
}
