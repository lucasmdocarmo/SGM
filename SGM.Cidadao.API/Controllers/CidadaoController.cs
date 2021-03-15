using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Cidadao.Application.Commands.Cidadao;
using SGM.Shared.Core.Application;
using SGM.Shared.Core.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Cidadao.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class CidadaoController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarCidadaoCommand> _commandCadastrar;

        public CidadaoController(ICommandHandler<CadastrarCidadaoCommand> commandCadastrar)
        {
            _commandCadastrar = commandCadastrar;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            return NoContent();
        }

        [HttpGet("{id}/Impostos")]
        public async Task<IActionResult> GetConsultarImpostosCidadao(Guid id)
        {
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCidadao(Guid id, [FromBody][Bind] EditarCidadaoCommand command)
        {
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(Notificacao), StatusCodes.Status412PreconditionFailed)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status504GatewayTimeout)]
        public async Task<IActionResult> CreateTodoItem([FromBody] CadastrarCidadaoCommand command)
        {
            var result = await _commandCadastrar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (result.Success)
            {
                return Ok(result.Result);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            return NoContent();
        }
    }
}
