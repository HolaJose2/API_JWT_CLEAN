using System.ComponentModel.DataAnnotations;

namespace PruebaUser.Core.DTOs
{
    public class LoginUsuarioDTO
    {
        [Required]
        public string Clave { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
