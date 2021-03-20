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
using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;
using SGM.Cidadao.Application.Queries.Results.Cidadao;
using SGM.Shared.Core.Queries.Handler;
using SGM.Cidadao.Infra.Repositories.Contracts;
using SGM.Cidadao.API.Extensions;
using SGM.Cidadao.Application.Queries.Cidadao;

namespace SGM.Cidadao.API.Controllers
{
    [AttributesExtensions]
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class CidadaoController : ControllerBase
    {
        private readonly ICommandHandler<CadastrarCidadaoCommand> _commandCadastrar;
        private readonly ICommandHandler<EditarCidadaoCommand> _commandEditar;
        private readonly ICommandHandler<DeletarCidadaoCommand> _commandDeletar;
        private readonly IQueryHandler<CidadaoQueryResult> _queryCidadaoCpf;
        private readonly IQueryHandler<ConsultarConsultaMedicaQuery> _queryCidadaoConsulta;
        private readonly ICidadaoRepository _cidadaoRepository;
        private readonly IImpostosRepository _impostosRepository;
        private readonly IContribuicaoRepository _contribuicaoRepository;
        private readonly IStatusContribuicaoRepository _statusRepository;

        public CidadaoController(ICommandHandler<CadastrarCidadaoCommand> commandCadastrar, ICommandHandler<EditarCidadaoCommand> commandEditar,
                                 ICommandHandler<DeletarCidadaoCommand> commandDeletar, IQueryHandler<CidadaoQueryResult> queryCidadaoCpf,
                                 ICidadaoRepository cidadaoRepository, IImpostosRepository impostosRepository, IContribuicaoRepository contribuicaoRepository, IStatusContribuicaoRepository statusRepository, IQueryHandler<ConsultarConsultaMedicaQuery> queryCidadaoConsulta)
        {
            _commandCadastrar = commandCadastrar;
            _commandEditar = commandEditar;
            _commandDeletar = commandDeletar;
            _queryCidadaoCpf = queryCidadaoCpf;
            _cidadaoRepository = cidadaoRepository;
            _impostosRepository = impostosRepository;
            _contribuicaoRepository = contribuicaoRepository;
            _statusRepository = statusRepository;
            _queryCidadaoConsulta = queryCidadaoConsulta;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cidadaoRepository.GetAll().ConfigureAwait(true);
            if (result is null) { return NoContent(); }
            return Ok(result);
        }

        [HttpGet("{cpf}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([Required][FromRoute][Bind] string cpf)
        {
            var result = await _queryCidadaoCpf.Handle(new CidadaoQueryResult() { CPF = cpf }).ConfigureAwait(true) as QueryResult;

            if (!result.Success)
            {
                return UnprocessableEntity(result.Messages);
            }
            return Ok(result.Result);
        }

        [HttpGet("{id}/Impostos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConsultarImpostosCidadao(Guid id)
        {
            var result = await _contribuicaoRepository.Search(x => x.CidadaoId == id).ConfigureAwait(true) as QueryResult;
            if (!result.Success)
            {
                return UnprocessableEntity(result.Messages);
            }
            return Ok(result.Result);
        }

        [HttpGet("{id}/Status")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStatusImpostosCidadao(Guid id)
        {
            return null;
        }

        [HttpGet("{id}/Consultas")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConsultarSaudeCidadao(ConsultarConsultaMedicaQuery query)
        {
            var result = await _queryCidadaoConsulta.Handle(query).ConfigureAwait(true) as QueryResult;

            if (!result.Success)
            {
                return UnprocessableEntity(result.Messages);
            }
            return Ok(result.Result);
        }


        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCidadao([FromRoute][Bind] Guid id, [FromBody][Bind] EditarCidadaoCommand command)
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

            var result = await _commandEditar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity();
            }
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody] CadastrarCidadaoCommand command)
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem([FromRoute][Bind][Required] Guid id)
        {
            var command = new DeletarCidadaoCommand() { Id = id };
            var result = await _commandDeletar.Handle(command).ConfigureAwait(true) as CommandResult;

            if (!result.Success)
            {
                return UnprocessableEntity();
            }
            return Ok(result.Result);
        }
    }
}
