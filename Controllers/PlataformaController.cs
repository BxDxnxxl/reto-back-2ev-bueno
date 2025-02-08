using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaService _service;

        public PlataformaController(IPlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plataforma>>> GetPlataformas()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plataforma>> GetPlataforma(int id)
        {
            var plataforma = await _service.GetByIdAsync(id);
            if (plataforma == null) return NotFound();
            return Ok(plataforma);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlataforma(Plataforma plataforma)
        {
            await _service.AddAsync(plataforma);
            return CreatedAtAction(nameof(GetPlataforma), new { id = plataforma.Id }, plataforma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlataforma(int id, Plataforma plataforma)
        {
            if (id != plataforma.Id) return BadRequest();

            var existingPlataforma = await _service.GetByIdAsync(id);
            if (existingPlataforma == null) return NotFound();

            await _service.UpdateAsync(plataforma);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlataforma(int id)
        {
            var plataforma = await _service.GetByIdAsync(id);
            if (plataforma == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
