using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : ControllerBase
    {
        private readonly IIdeaService _service;

        public IdeaController(IIdeaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ideas>>> GetIdeas()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreateIdea(Ideas idea)
        {
            await _service.AddAsync(idea);
            return CreatedAtAction(nameof(GetIdeas), new { id = idea.Id }, idea);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("con-usuarios")]
        public async Task<ActionResult<List<IdeaDto>>> GetAllIdeasConNombreUsuario()
        {
            var ideas = await _service.GetAllIdeasConNombreUsuarioAsync();
            return Ok(ideas);
        }
    }
}
