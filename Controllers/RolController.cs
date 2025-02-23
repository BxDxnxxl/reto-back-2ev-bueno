using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;

        public RolController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var rol = await _service.GetByIdAsync(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRol(Rol rol)
        {
            await _service.AddAsync(rol);
            return CreatedAtAction(nameof(GetRol), new { id = rol.Id }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, Rol rol)
        {
            if (id != rol.Id) return BadRequest();

            var existingRol = await _service.GetByIdAsync(id);
            if (existingRol == null) return NotFound();

            await _service.UpdateAsync(rol);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _service.GetByIdAsync(id);
            if (rol == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("asignarRoles")]
        public async Task<IActionResult> AssignRolesToUsuario([FromBody] UsuarioRolDto usuarioRolDto)
        {
            await _service.asignarRolesAUsuarios(usuarioRolDto);
            return Ok(new { message = "Roles asignados correctamente" });
        }
    }
}
