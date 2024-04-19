using PruebaUser.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaUser.Core.DTOs
{
    public class UsuarioCreacionDTO
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El documento es requerido.")]
        public long Documento { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(10, ErrorMessage = "La contraseña debe tener al menos 10 caracteres.")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido.")]
        public int TipoDocumentoId { get; set; }

    }
}
