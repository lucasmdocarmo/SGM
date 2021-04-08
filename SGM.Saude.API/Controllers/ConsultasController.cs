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
using System.Linq;
using SGM.Saude.Application.Queries;

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
        private readonly IMedicosRepository _repositoryMedico;
        private readonly IPacienteRepository _pacienteRepository;

        public ConsultasController(ICommandHandler<CadastrarConsultaCommand> commandCadastrar, ICommandHandler<EditarConsultaCommand> commandEditar,
            ICommandHandler<DeletarConsultaCommand> commandDeletar, IConsultaRepository repository, IMedicosRepository repositoryMedico, IPacienteRepository pacienteRepository)
        {
            _commandCadastrar = commandCadastrar;
            _commandEditar = commandEditar;
            _commandDeletar = commandDeletar;
            _repository = repository;
            _repositoryMedico = repositoryMedico;
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet("Agendas")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemsConsultas()
        {
            var result = await _repository.Search(x => x.PacienteId == null && !x.Reservado).ConfigureAwait(true);

            if (result is null) { return NoContent(); }

            var listMedicos = new List<ConsultasSemPacientesQuery>();
            foreach (var item in result.ToList())
            {
                listMedicos.Add(new ConsultasSemPacientesQuery()
                {
                    Especialidade = item.Especialidade,
                    DataConsulta = item.DataConsulta,
                    Descricaco = item.Descricao,
                    Medico = _repositoryMedico.GetById(item.MedicoId).Result.Nome,
                    CRM = _repositoryMedico.GetById(item.MedicoId).Result.CRM,
                });
            }
            return Ok(listMedicos);
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
            var resultado = new PacienteQuery();
            var result = await _repository.Search(x=>x.Paciente.CPF == cpf).ConfigureAwait(true);
            if (result is null) { return NoContent(); }

            var consultas = result.Where(x => x.DataConsulta > DateTime.Now).ToList();
            foreach (var item in consultas)
            {
                var medico = await _repositoryMedico.GetById(item.MedicoId).ConfigureAwait(true);

                resultado.Consultas.Add(new PacienteConsultas()
                {
                    Medico = medico.Nome,
                    CRM = medico.CRM,
                    DataConsulta = item.DataConsulta,
                    Confirmada = item.Reservado,
                    Descricao = item.Descricao,
                    Especialidade = item.Especialidade,
                    InformacoesMedicas = item.InformacoesMedicas,
                    MedicoId = item.MedicoId,
                    PacienteId = item.PacienteId
                });
            }

            return Ok(resultado);
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
        [HttpPut("{id}/Desmarcar")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DesmarcarUpdateTodoItem([FromRoute][Bind] Guid id)
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
           
            var consulta = await _repository.GetById(id).ConfigureAwait(true);

            consulta.PacienteId = null;
            consulta.Reservado = false;

            await _repository.Update(consulta);
            await _repository.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}/Marcar")]
        [Authorize(Roles = "Clinica, Gestao, Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarcarUpdateTodoItem([FromRoute][Bind] Guid id, [FromBody][Bind] string cpf)
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
            var paciente = await _pacienteRepository.Search(x => x.CPF == cpf).ConfigureAwait(true);
            if (paciente is null) { return UnprocessableEntity("Paciente não encontrado."); }

            var entityPaciente = paciente.FirstOrDefault();

            var consulta = await _repository.GetById(id).ConfigureAwait(true);

            consulta.PacienteId = entityPaciente.Id;
            consulta.Reservado = true;

            await _repository.Update(consulta);
            await _repository.SaveChanges();

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
