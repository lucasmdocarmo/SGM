using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Events.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Events.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public readonly IServicesProducer _producer;

        public EventsController(IServicesProducer cidadaoProducer)
        {
            _producer = cidadaoProducer;
        }

        [HttpPost("Cidadao")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostCidadao([FromBody] CidadaoWrapper payload)
        {
            var result = await _producer.SendCidadaoByKafka(payload).ConfigureAwait(true);
            if (!result) { return UnprocessableEntity("Erro ao replicar Cidadao"); }

            return Created("Cidadao Criado", payload);
        }
        [HttpPost("Paciente")] 
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostPaciente([FromBody] PacienteWrapper payload)
        {
            var result = await _producer.SendPacienteByKafka(payload).ConfigureAwait(true);
            if (!result) { return UnprocessableEntity("Erro ao replicar Paciente"); }

            return Created("Paciente Criado", payload);
        }
        [HttpPost("Usuario")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioWrapper payload)
        {
            var result = await _producer.SendUsuarioByKafka(payload).ConfigureAwait(true);
            if (!result) { return UnprocessableEntity("Erro ao replicar Usuario"); }

            return Created("Usuario Criado", payload);
        }
        [HttpPost("Consulta")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostConsulta([FromBody] ConsultaWrapper payload)
        {
            var result = await _producer.SendConsultaByKafka(payload).ConfigureAwait(true);
            if (!result) { return UnprocessableEntity("Erro ao replicar Consulta"); }

            return Created("Consulta Marcada", payload);
        }
    }
}
