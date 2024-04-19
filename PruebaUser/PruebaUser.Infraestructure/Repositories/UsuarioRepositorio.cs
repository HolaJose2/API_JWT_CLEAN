using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;
using PruebaUser.Core.Interfaces;
using PruebaUser.Core.Repositories;
using PruebaUser.Infraestructure.Services;
using Serilog;


namespace PruebaUser.Infraestructure.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UserDBContext context;
        private readonly IMapper mapper;
        private readonly IPasswordManager passwordManager;

        public UsuarioRepositorio(UserDBContext context, IMapper mapper, IPasswordManager passwordManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordManager = passwordManager;
        }

        public async Task<bool> ValidarSiExisteCorreo(string emailNuevo, int? idUsuarioActualizar = null)
        {
            try
            {
                // Verificar si existe algún usuario con el mismo correo electrónico
                var existeUsuario = await context.Set<Usuario>()
                    .AnyAsync(usuario => usuario.Email == emailNuevo && (idUsuarioActualizar == null || usuario.Id != idUsuarioActualizar));

                return existeUsuario;
            }
            catch (Exception ex)
            {
                Log.Error($"Error al verificar la existencia de usuario con correo {emailNuevo}: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> ValidarSiExisteDocumento(long documento)
        {
            try
            {
                return await context.Set<Usuario>().AnyAsync(usuario => usuario.Documento == documento);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al verificar la existencia de usuario con documento {documento}: {ex.Message}");
                throw;
            }
        }

        public async Task<UsuarioDTO> ConsultarUsuarioPorId(int Id)
        {
            try
            {
                var usuario = await context.Set<Usuario>().Include(u => u.TipoDocumento).FirstOrDefaultAsync(u => u.Id == Id);

                if (usuario == null) return null;

                return mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al consultar usuario con ID {Id}: {ex.Message}");

                throw;
            }
        }

        public async Task<IEnumerable<UsuarioDTO>> ListarUsuarios()
        {
            try
            {
                var usuarios = await context.Set<Usuario>().Include(usuario => usuario.TipoDocumento).ToListAsync();
                return mapper.Map<IList<UsuarioDTO>>(usuarios);

            }
            catch (Exception ex)
            {
                Log.Error($"Error al listar los usuarios: {ex.Message}");

                throw;
            }

        }

        public async Task<bool> EliminarUsuarioPorId(int Id)
        {
            try
            {
                var usuarioAEliminar = await context.Set<Usuario>().FindAsync(Id);

                if (usuarioAEliminar == null) return false;


                context.Set<Usuario>().Remove(usuarioAEliminar);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Error al eliminar el Usuario: {ex.Message}");

                throw;
            }
        }


        public async Task<(UsuarioDTO? UsuarioDTO, string mensaje)> CrearUsuario(UsuarioCreacionDTO usuarioCreacionDTO)
        {
            try
            {
                var tipoDocumento = await context.Set<TipoDocumento>().FindAsync(usuarioCreacionDTO.TipoDocumentoId);
                var existeDocumento = await ValidarSiExisteDocumento(usuarioCreacionDTO.Documento);
                var existeCorreo = await ValidarSiExisteCorreo(usuarioCreacionDTO.Email);

                if (tipoDocumento == null) return (null, "Tipo de documento Invalido.");
                if (existeDocumento) return (null, "Ya existe un usuario con este documento");
                if (existeCorreo) return (null, "Ya existe un usuario con este Correo Electronico.");

                var entidadUsuario = mapper.Map<Usuario>(usuarioCreacionDTO);
                entidadUsuario.TipoDocumento = tipoDocumento;
                entidadUsuario.Clave = passwordManager.HashPassword(entidadUsuario.Clave);

                await context.AddAsync(entidadUsuario);
                await context.SaveChangesAsync();

                var usuarioDTO = mapper.Map<UsuarioDTO>(entidadUsuario);

                return (usuarioDTO, "Usuario Creado Satisfactoriamente.");

            }
            catch (Exception ex)
            {
                Log.Error($"Error al crear usuario: {ex.Message}");
                throw;
            }
        }

        public async Task<(UsuarioDTO? UsuarioDTO, string mensaje)> ActualizarUsuario(int Id, UsuarioCreacionDTO usuarioActualizado)
        {
            try
            {
                var usuarioExistente = await context.Set<Usuario>().FindAsync(Id);
                if (usuarioExistente == null) return (null, "No existe el usuario que desea actualizar.");

                var tipoDocumento = await context.Set<TipoDocumento>().FindAsync(usuarioActualizado.TipoDocumentoId);
                if (tipoDocumento == null) return (null, "Tipo de documento Invalido.");

                var existeDocumento = await ValidarSiExisteDocumento(usuarioActualizado.Documento);
                if (existeDocumento) return (null, "Ya existe un usuario con este documento");

                var existeCorreo = await ValidarSiExisteCorreo(usuarioActualizado.Email, Id);
                if (existeCorreo) return (null, "Ya existe un usuario con este Correo Electronico.");

                usuarioExistente.Nombre = usuarioActualizado.Nombre;
                usuarioExistente.Apellido = usuarioActualizado.Apellido;
                usuarioExistente.Documento = usuarioActualizado.Documento;
                usuarioExistente.TipoDocumentoId = usuarioActualizado.TipoDocumentoId;
                usuarioExistente.Email = usuarioActualizado.Email;
                usuarioExistente.Clave = passwordManager.HashPassword(usuarioActualizado.Clave);
                usuarioExistente.TipoDocumento = tipoDocumento;


                await context.SaveChangesAsync();
                var usuarioDto = mapper.Map<UsuarioDTO>(usuarioExistente);
                return (usuarioDto, "Usuario Actualizado Satisfactoriamente.");

            }
            catch (Exception ex)
            {
                Log.Error($"Error al actualizar el Usuario: {ex.Message}");

                throw;
            }
        }
    }
}
