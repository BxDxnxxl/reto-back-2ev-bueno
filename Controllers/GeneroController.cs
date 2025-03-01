using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _service;

        public GeneroController(IGeneroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> GetGeneros()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _service.GetByIdAsync(id);
            if (genero == null) return NotFound();
            return Ok(genero);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGenero(Genero genero)
        {
            await _service.AddAsync(genero);
            return CreatedAtAction(nameof(GetGenero), new { id = genero.Id }, genero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenero(int id, Genero genero)
        {
            if (id != genero.Id) return BadRequest();

            var existingGenero = await _service.GetByIdAsync(id);
            if (existingGenero == null) return NotFound();

            await _service.UpdateAsync(genero);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _service.GetByIdAsync(id);
            if (genero == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("top5")]
        public async Task<ActionResult<List<Genero>>> GetTop5Genres()
        {
            var result = await _service.GetTop5GenresAsync();
            return Ok(result);
        }
    }
}
