using AutoMapper;
using PruebaUser.Core.DTOs;
using PruebaUser.Core.Entities;

namespace PruebaUser.Infraestructure.Mappings
{
    public class Perfiles: Profile
    {
        public Perfiles()
        {
            CreateMap<UsuarioCreacionDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<TipoDocumento, TipoDocumentoDTO>();
        }
    }
}
