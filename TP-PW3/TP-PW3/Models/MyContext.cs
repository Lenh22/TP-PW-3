using Microsoft.EntityFrameworkCore;

namespace TP_PW3.Models
{
    public class MyContext : DbContext
    {

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-EPGMG0R\\SQLEXPRESS;Initial Catalog=blazorBD;User ID=sa;Password=sabrinakilian1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.UseSqlServer("Server=blazorservidor.database.windows.net;Initial Catalog=blazorBD;Persist Security Info=False;User ID=web3;Password=2doCuatri;MultipleActiveResultSets=True;App=EntityFramework;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
