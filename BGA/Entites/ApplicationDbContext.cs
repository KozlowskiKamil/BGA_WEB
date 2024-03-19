using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace BGA.Entites
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Repair> Repair { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=PLKWIM0SQLV02B\\ENG;Database=TE_CPK;Trusted_Connection=True");
            //optionsBuilder.UseMySql("Server=mysql-kamil.alwaysdata.net;Database=kamil_printer;Integrated Security=True;User ID=kamil;Password=;"");
            //optionsBuilder.UseSqlServer("Server=ysql-kamil.alwaysdata.net;Database=kamil_printer;Integrated Security=True;User ID=kamil;Password=;");       
            //optionsBuilder.UseSqlServer("Server=PLKWIM0SQLV02B\\ENG;Database=TE_CPK;Integrated Security=True;User ID=neumannp;Password=;");

            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Bga;Trusted_Connection=True");
            optionsBuilder.UseSqlServer("Server = PLKWIM0SQLV02B\\ENG; Database = TE_CPK; Integrated Security = True; User ID = JABIL\\smckwi_bgaprd$");
            base.OnConfiguring(optionsBuilder);
        }
    }

}