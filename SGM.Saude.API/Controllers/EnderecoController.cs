using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Saude.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, string todoItemDTO)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(string todoItemDTO)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            return NoContent();
        }
    }
}
