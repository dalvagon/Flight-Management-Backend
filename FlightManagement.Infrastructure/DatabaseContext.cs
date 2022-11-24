using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Address> Addresses { get; private set; }
        public DbSet<Administrator> Administrators { get; private set; }
        public DbSet<Airport> Airports { get; private set; }
        public DbSet<Allergy> Allergies { get; private set; }
        public DbSet<Baggage> Baggages { get; private set; }
        public DbSet<Company> Companies { get; private set; }
        public DbSet<Flight> Flights { get; private set; }
        public DbSet<Passenger> Passengers { get; private set; }
        public DbSet<Person> People { get; private set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = FlightManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var person1 = Person.Create("Leahu", "Vlad", new DateTime(2002, 6, 24), "Male").Entity;

            var person2 = Person.Create("Ion", "Titi", new DateTime(1985, 4, 10), "Male").Entity;

            var company1 = new Company("Atlas", new DateTime(1998, 12, 1));

            modelBuilder.Entity<Person>().HasData(new List<Person> { person1, person2 });

            modelBuilder.Entity<Company>().HasData(new List<Company> { company1 });
        }
    }
}