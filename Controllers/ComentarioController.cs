using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _service;

        public ComentarioController(IComentarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comentario>>> GetComentarios()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _service.GetByIdAsync(id);
            if (comentario == null) return NotFound();
            return Ok(comentario);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComentario(Comentario comentario)
        {
            await _service.AddAsync(comentario);
            return CreatedAtAction(nameof(GetComentario), new { id = comentario.Id }, comentario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComentario(int id, Comentario comentario)
        {
            if (id != comentario.Id) return BadRequest();

            var existingComentario = await _service.GetByIdAsync(id);
            if (existingComentario == null) return NotFound();

            await _service.UpdateAsync(comentario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _service.GetByIdAsync(id);
            if (comentario == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("videojuego/{videojuegoId}")]
        public async Task<ActionResult<List<ComentarioDto>>> GetComentariosByVideojuego(int videojuegoId)
        {
            var comentarios = await _service.GetComentariosByVideojuegoAsync(videojuegoId);
            return Ok(comentarios);
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeComentario(int id)
        {
            bool success = await _service.UpdateLikesDislikesAsync(id, true);
            if (!success) return NotFound("Comentario no encontrado.");
            return Ok("Like agregado con éxito.");
        }

        [HttpPut("dislike/{id}")]
        public async Task<IActionResult> DislikeComentario(int id)
        {
            bool success = await _service.UpdateLikesDislikesAsync(id, false);
            if (!success) return NotFound("Comentario no encontrado.");
            return Ok("Dislike agregado con éxito.");
        }
    }
}
