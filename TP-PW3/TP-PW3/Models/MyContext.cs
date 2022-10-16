using Microsoft.EntityFrameworkCore;

namespace TP_PW3.Models
{
    public class MyContext : DbContext
    {

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EPGMG0R\\SQLEXPRESS;Initial Catalog=blazorBD;User ID=sa;Password=sabrinakilian1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
