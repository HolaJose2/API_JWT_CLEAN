using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;

namespace PruebaUser.Core.Repositories
{
    public interface IUsuarioRepositorio
    {
        public Task<IEnumerable<UsuarioDTO>> ListarUsuarios();
        public Task<UsuarioDTO> ConsultarUsuarioPorId(int Id);
        public Task<bool> EliminarUsuarioPorId(int Id);
        public Task<(UsuarioDTO? UsuarioDTO,string mensaje)> CrearUsuario(UsuarioCreacionDTO usuarioCreacionDTO);
        public Task<bool> ValidarSiExisteDocumento(long documento);
        public Task<bool> ValidarSiExisteCorreo(string emailNuevo, int? idUsuarioActualizar = null);
        public Task<(UsuarioDTO? UsuarioDTO, string mensaje)> ActualizarUsuario(int Id, UsuarioCreacionDTO usuarioActualizado);
    }
}
