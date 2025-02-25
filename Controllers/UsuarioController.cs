using Microsoft.AspNetCore.Mvc;
using Models;
using Videojuegos.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        
            public async Task<int> AddAsync(Usuario usuario)
            {
                usuario.Id = await _service.AddAsync(usuario);
                return usuario.Id;
            }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            await _service.UpdateAsync(id, usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null){
             return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<Usuario>>> GetUsuariosByNombre([FromQuery] string nombre)
        {
            var usuarios = await _service.GetUsuariosByNombreAsync(nombre);
            return Ok(usuarios);
        }

        [HttpPost("CrearDesdeLogin")]
        public async Task<ActionResult<Usuario>> CreateUsuarioDesdeLogin([FromBody] UsuarioCreacionBaseDto usuario)
        {
            int usuarioCreadoId = await _service.CreacionBasicaAsync(usuario);
            
            var usuarioCreado = await _service.GetByIdAsync(usuarioCreadoId);
            
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCreado.Id }, usuarioCreado);
        }


        [HttpGet("usuarios-con-roles")]
        public async Task<ActionResult<List<UserInfoRoles>>> GetUsuariosConRoles()
        {
            var usuarios = await _service.GetUsuariosConRolesAsync();
            return Ok(usuarios);
        }

       [HttpGet("detalle/{usuarioId}")]
        public async Task<IActionResult> GetUsuarioConRolesById(int usuarioId)
        {
            var usuario = await _service.GetUsuarioConRolesByIdAsync(usuarioId);
            return Ok(usuario);
        }
    }
}
