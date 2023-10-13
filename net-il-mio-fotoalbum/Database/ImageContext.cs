using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Database
{
    public class ImageContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<FormContent> FormContents { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=ImageDB; TrustServerCertificate=True; Integrated Security=True");
        }
        // QUESTO METODO ^^^^^^^^^ IMPOSTA LA STRINGA DI CONNESSIONE importante:  TrustServerCertificate=True;
    }
}
