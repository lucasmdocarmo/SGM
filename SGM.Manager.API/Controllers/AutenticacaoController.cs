using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Identity.Service;
using SGM.Manager.Infra.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Manager.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoController(IFuncionarioRepository funcionarioRepository, IUsuarioRepository usuarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("Login/Funcionario")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Login([Required]string login, [Required] string senha)
        {
            var funcionarioEntity = await _funcionarioRepository.GetFuncionario(login, senha).ConfigureAwait(true);
            if(funcionarioEntity is null) { return NotFound("Funcionário/Senha Não encontrado."); }

            var token = IdentityTokenService.GenerateTokenFuncionario(funcionarioEntity.Nome, funcionarioEntity.Login, funcionarioEntity.TipoFuncionario);
            return Ok(token);
        }
        [HttpPost]
        [Route("Login/Usuario")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Usuario([Required] string login, [Required] string senha)
        {
            var usuarioEntity = await _usuarioRepository.GetUsuario(login, senha).ConfigureAwait(true);
            if (usuarioEntity is null) { return NotFound("Usuário Não encontrado."); }

            var tokenUsuario = IdentityTokenService.GenerateTokenUsuario(usuarioEntity.Nome, usuarioEntity.Login, usuarioEntity.TipoUsuario);
            return Ok(tokenUsuario);
        }
    }
}
