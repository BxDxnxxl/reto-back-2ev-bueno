using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniaController : ControllerBase
    {
        private readonly ICompaniaService _service;

        public CompaniaController(ICompaniaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Compania>>> GetCompanias()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compania>> GetCompania(int id)
        {
            var compania = await _service.GetByIdAsync(id);
            if (compania == null) return NotFound();
            return Ok(compania);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompania(Compania compania)
        {
            await _service.AddAsync(compania);
            return CreatedAtAction(nameof(GetCompania), new { id = compania.Id }, compania);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompania(int id, Compania compania)
        {
            if (id != compania.Id) return BadRequest();

            var existingCompania = await _service.GetByIdAsync(id);
            if (existingCompania == null) return NotFound();

            await _service.UpdateAsync(compania);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompania(int id)
        {
            var compania = await _service.GetByIdAsync(id);
            if (compania == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("top5")]
        public async Task<ActionResult<List<Compania>>> GetTop5Companies()
        {
            var result = await _service.GetTop5CompaniesAsync();
            return Ok(result);
        }
    }
}
