﻿using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Address> Addresses { get; private set; }
    public DbSet<Administrator> Administrators { get; private set; }
    public DbSet<Airport> Airports { get; private set; }
    public DbSet<Allergy> Allergies { get; private set; }
    public DbSet<Baggage> Baggages { get; private set; }
    public DbSet<City> Cities { get; private set; }
    public DbSet<Country> Countries { get; private set; }
    public DbSet<Flight> Flights { get; private set; }
    public DbSet<Passenger> Passengers { get; private set; }
    public DbSet<Person> People { get; private set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .Property(c => c.Id)
            .HasConversion<string>();

        modelBuilder.Entity<City>()
            .Property(c => c.Id)
            .HasConversion<string>();
    }
}