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
        }
    }
}