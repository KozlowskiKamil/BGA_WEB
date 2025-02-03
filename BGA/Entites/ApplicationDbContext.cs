using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace BGA.Entites
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Repair> Repair { get; set; }
        public DbSet<Rma> Rma { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server = PLKWIM0SQLV02B\\ENG; Database = TE_CPK; Integrated Security = True; User ID = JABIL\\kozlowsk");
            //optionsBuilder.UseSqlServer("Server = PLKWIM0SQLV02B\\ENG; Database = TE_CPK; Integrated Security = True; User ID = JABIL\\Neumann");

            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Bga;Trusted_Connection=True");
            optionsBuilder.UseSqlServer("Server = PLKWIM0SQLV02B\\ENG; Database = TE_CPK; Integrated Security = True; User ID = JABIL\\smckwi_bgaprd$");
            base.OnConfiguring(optionsBuilder);
        }
    }

}