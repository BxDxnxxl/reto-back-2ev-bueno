using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideojuegosController : ControllerBase
    {
        private readonly IVideojuegoService _service;

        public VideojuegosController(IVideojuegoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Videojuego>>> GetVideojuegos()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Videojuego>> GetVideojuego(int id)
        {
            var videojuego = await _service.GetByIdAsync(id);
            if (videojuego == null) return NotFound();
            return Ok(videojuego);
        }

        [HttpPost]
        public async Task<ActionResult> CreateVideojuego(Videojuego videojuego)
        {
            await _service.AddAsync(videojuego);
            return CreatedAtAction(nameof(GetVideojuego), new { id = videojuego.Id }, videojuego);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideojuego(int id, Videojuego videojuego)
        {
            await _service.UpdateAsync(videojuego);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideojuego(int id)
        {
            var videojuego = await _service.GetByIdAsync(id);
            if (videojuego == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/detalle")]
        public async Task<ActionResult<VideojuegoDetalleDto>> GetVideojuegoDetalle(int id)
        {
            var detalle = await _service.GetDetalleByIdAsync(id);
            if (detalle == null) return NotFound();
            return Ok(detalle);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Videojuego>>> FiltrarVideojuegos([FromQuery] string? compania, [FromQuery] string? genero, [FromQuery] string? plataforma)
        {
            var videojuegos = await _service.FiltrarVideojuegosAsync(compania, genero, plataforma);
            return Ok(videojuegos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<Videojuego>>> BuscarVideojuegos([FromQuery] string filtro)
        {
            var videojuegos = await _service.BuscarVideojuegosAsync(filtro);
            return Ok(videojuegos);
        }

        [HttpGet("top5")]
        public async Task<ActionResult<List<Videojuego>>> GetTop5MejoresVideojuegosValorados()
        {
            var videojuegos = await _service.GetTop5MejoresVideojuegosValoradosAsync();
            return Ok(videojuegos);
        }
    }
}
