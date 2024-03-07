using Microsoft.EntityFrameworkCore;

namespace BGA.Entites
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Repair> Repair { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Bga;Trusted_Connection=True");
            optionsBuilder.UseSqlServer("Server=PLKWIM0SQLV02B\\ENG;Database=TE_CPK;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer("Server=PLKWIM0SQLV02B\\ENG;Database=TE_CPK;Integrated Security=False;User ID=neumannp;Password=;");
            base.OnConfiguring(optionsBuilder);
        }
    }

}