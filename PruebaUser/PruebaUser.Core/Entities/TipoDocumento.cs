using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaUser.Core.Entities
{
    [Table("TipoDocumento")]
    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario>? Usuarios { get; set; }
    }
}
