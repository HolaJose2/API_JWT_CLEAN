using PruebaUser.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaUser.Core.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Documento { get; set; }
        public string Email { get; set; }
        public int TipoDocumentoId { get; set; }
        public TipoDocumentoDTO TipoDocumento { get; set; }


    }
}
