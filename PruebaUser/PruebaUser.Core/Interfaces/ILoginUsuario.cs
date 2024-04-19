using PruebaUser.Core.DTOs;

namespace PruebaUser.Core.Interfaces
{
    public interface ILoginUsuario
    {
        Task<LoginResponse> DevolverToken(LoginUsuarioDTO loginUsuarioDTO);
    }
}
