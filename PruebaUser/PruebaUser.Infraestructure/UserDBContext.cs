using Microsoft.EntityFrameworkCore;
using PruebaUser.Core.Entities;

namespace PruebaUser.Infraestructure
{
    public class UserDBContext: DbContext
    {
        public UserDBContext(DbContextOptions options) : base(options){}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set;}
    }
}
