using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaUser.Core.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido{ get; set; }
        public long Documento { get; set; }
        public string Clave { get; set;}
        public string Email { get; set; }
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }



    }
}
