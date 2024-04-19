using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;
using PruebaUser.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaUser.Infraestructure.Services
{
    public class LoginUsuario : ILoginUsuario
    {
        private readonly UserDBContext context;
        private readonly IConfiguration configuration;
        private readonly IPasswordManager passwordManager;

        public LoginUsuario(UserDBContext context, IConfiguration configuration, IPasswordManager passwordManager)
        {
            this.context = context;
            this.configuration = configuration;
            this.passwordManager = passwordManager;
        }

        private string GenerarToken(int idUsuario)
        {
            var key = configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()));

            var credencialesToken = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);

            var detallesToken = new SecurityTokenDescriptor
            {
                Subject = claims,

                Expires = DateTime.UtcNow.AddMinutes(2), //El tiempo que durará el Token
                SigningCredentials = credencialesToken,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(detallesToken);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }


        public async Task<LoginResponse> DevolverToken(LoginUsuarioDTO loginUsuarioDTO)
        {
            var usuario_encontrado = await context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == loginUsuarioDTO.Email);

            if (usuario_encontrado == null) return new LoginResponse(null, false, "No existe un usuario con estas credenciales");

            if (!passwordManager.VerifyPassword(loginUsuarioDTO.Clave, usuario_encontrado.Clave))
                return new LoginResponse(null, false, "La contraseña proporcionada es incorrecta");

            string tokenCreado = GenerarToken(usuario_encontrado.Id);

            return new LoginResponse(tokenCreado, true, "Ok");
        }
    }
}
