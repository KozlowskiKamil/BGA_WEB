using Microsoft.EntityFrameworkCore;

namespace BGA.Entites
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Repair> Repair { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Bga;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }
    }

    
}
