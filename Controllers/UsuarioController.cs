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
        public async Task<ActionResult> CreateUsuario(Usuario usuario)
        {
            await _service.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();

            var existingUsuario = await _service.GetByIdAsync(id);
            
            if (existingUsuario == null){ 
                return NotFound();
            }
            await _service.UpdateAsync(usuario);
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

        [HttpGet("login")]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _service.LoginAsync(request.Username, request.Password);
            return Ok(usuario);
        }

        [HttpPost("CrearDesdeLogin")]
        public async Task<ActionResult> CreateUsuarioDesdeLogin(UsuarioCreacionBaseDto usuario, Usuario usuarioCreado)
        {
            await _service.CreacionBasicaAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCreado.Id }, usuarioCreado);
        }
    }
}
