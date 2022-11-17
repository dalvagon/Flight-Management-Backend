using FlightManagement.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; private set; }
        public DbSet<Administrator> Administrators { get; private set; }
        public DbSet<Company> Companies { get; private set; }

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
            var person1 = Person.Create(
                "Leahu",
                "Vlad",
                new DateTime(2002, 6, 24),
                "Male"
            ).Entity;

            var person2 = Person.Create(
                "Ion",
                "Titi",
                new DateTime(1985, 4, 10),
                "Male"
            ).Entity;

            var company1 = Company.Create(
                "Atlas",
                new DateTime(1999, 5, 9)
            ).Entity;

            modelBuilder.Entity<Person>().HasData(
                    new List<Person> { person1, person2 }
                );

            modelBuilder.Entity<Company>().HasData(
                    new List<Company> { company1 }
                );
        }
    }
}
