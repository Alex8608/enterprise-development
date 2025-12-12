using AirlineCompany.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Data;

/// <summary>
/// Represents the database context for the Airline Company application.
/// Configures entity mappings and relationships between entities.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the DbSet for aircraft families.
    /// </summary>
    public DbSet<AircraftFamily> AircraftFamilies { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for aircraft models.
    /// </summary>
    public DbSet<AircraftModel> AircraftModels { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for flights.
    /// </summary>
    public DbSet<Flight> Flights { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for passengers.
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for tickets.
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Configures the entity mappings and relationships for the database model.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<AircraftFamily>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Manufacturer).IsRequired();

            entity.HasData(DataSeeder.AircraftFamilies);
        });

        modelBuilder.Entity<AircraftModel>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Range).IsRequired();
            entity.Property(x => x.PassengerCapacity).IsRequired();
            entity.Property(x => x.CargoCapacity).IsRequired();

            entity
                .HasOne(x => x.AircraftFamily)
                .WithMany(x => x.Models)
                .HasForeignKey(x => x.AircraftFamilyId)
                .IsRequired();

            entity.HasData(DataSeeder.AircraftModels);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).IsRequired();
            entity.Property(x => x.DepartureCity).IsRequired();
            entity.Property(x => x.ArrivalCity).IsRequired();
            entity.Property(x => x.DepartureDate).IsRequired();
            entity.Property(x => x.ArrivalDate).IsRequired();
            entity.Property(x => x.Duration).IsRequired();

            entity
                .HasOne(x => x.AircraftModel)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.AircraftModelId)
                .IsRequired();

            entity.HasData(DataSeeder.Flights);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.PassportNumber).IsRequired();
            entity.Property(x => x.FullName).IsRequired();
            entity.Property(x => x.DateOfBirth).IsRequired();

            entity.HasIndex(x => x.PassportNumber).IsUnique();

            entity.HasData(DataSeeder.Passengers);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SeatNumber).IsRequired();
            entity.Property(x => x.HasHandLuggage).IsRequired();
            entity.Property(x => x.BaggageWeight).IsRequired();

            entity
                .HasOne(x => x.Flight)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.FlightId)
                .IsRequired();

            entity
                .HasOne(x => x.Passenger)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.PassengerId)
                .IsRequired();

            entity.HasIndex(x => new { x.FlightId, x.SeatNumber }).IsUnique();

            entity.HasData(DataSeeder.Tickets);
        });
    }
}