using Microsoft.AspNetCore.Mvc;
using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;
using PruebaUser.Core.Interfaces;
using PruebaUser.Core.Repositories;

namespace PruebaUser.Api.Controllers
{
    [ApiController]
    [Route("api/autenticar")]
    public class LoginUsuarioController : ControllerBase
    {
        private readonly ILoginUsuario servicio;

        public LoginUsuarioController(ILoginUsuario servicio)
        {
            this.servicio = servicio;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Autenticar([FromBody] LoginUsuarioDTO loginUsuarioDTO)
        {
            var resultado_autorizacion = await servicio.DevolverToken(loginUsuarioDTO);
            if (resultado_autorizacion.Resultado == false) return Unauthorized(resultado_autorizacion.Mensaje);

            return Ok(resultado_autorizacion);
        }

        
    }
}
