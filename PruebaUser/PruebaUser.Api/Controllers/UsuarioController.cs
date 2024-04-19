using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;
using PruebaUser.Core.Repositories;

namespace PruebaUser.Api.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio repositorio;

        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ListarUsuarios()
        {
            var usuarios = await repositorio.ListarUsuarios();
            if (usuarios == null) return NotFound("No hay usuarios registrados.");

            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}", Name = "obtenerUsuario")]
        public async Task<ActionResult<UsuarioDTO>> ConsultarUsuario(int id)
        {
            var usuario = await repositorio.ConsultarUsuarioPorId(id);
            if (usuario == null) return NotFound($"No existe el usuario con el id: {id}.");

            return Ok(usuario);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var exito = await repositorio.EliminarUsuarioPorId(id);

            if (!exito) return NotFound($"No existe el usuario con el id: {id}.");

            return Ok($"El Usuario con id {id} ha sido eliminado satisfactoriamente");
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CrearUsuario([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var (usuarioDTO, mensaje) = await repositorio.CrearUsuario(usuarioCreacionDTO);

            if (usuarioDTO == null) return BadRequest(mensaje);

            return CreatedAtRoute("obtenerUsuario", new { id = usuarioDTO.Id }, usuarioDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> ActualizarUsuario(int id, [FromBody] UsuarioCreacionDTO usuarioActualizado)
        {
            var (usuarioDTO, mensaje) = await repositorio.ActualizarUsuario(id, usuarioActualizado);

            if (usuarioDTO == null) return BadRequest(mensaje);

            return Ok(usuarioDTO);
        }
    }
}
