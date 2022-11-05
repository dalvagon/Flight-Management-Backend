using Microsoft.EntityFrameworkCore;

namespace FlightManagement.API.Data
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;" +
            //    "Initial Catalog=[AbsoluteFolderPath]\\SHELTER.MDF;" +
            //    "Integrated Security=True;Connect Timeout=30;Encrypt=False" +
            //    "TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
            //    "MultiSubnetFailover=False");

            optionsBuilder.UseSqlite("Data Source = FlightManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
