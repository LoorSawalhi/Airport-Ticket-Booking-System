namespace Infrastructre.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);
        builder.HasMany(f => f.ClassFlights)
            .WithOne(cf => cf.Flight)
            .HasForeignKey(cf => cf.FlightId);
        // Other configurations...
    }
}

public class FlightClassConfiguration : IEntityTypeConfiguration<FlightClass>
{
    public void Configure(EntityTypeBuilder<FlightClass> builder)
    {
        builder.HasKey(fc => fc.Id);
        builder.HasMany(fc => fc.ClassFlights)
            .WithOne(cf => cf.FlightClass)
            .HasForeignKey(cf => cf.FlightClassId);
        // Other configurations...
    }
}

public class ClassFlightConfiguration : IEntityTypeConfiguration<ClassFlight>
{
    public void Configure(EntityTypeBuilder<ClassFlight> builder)
    {
        builder.HasKey(cf => new { cf.FlightClassId, cf.FlightId });
        builder.HasOne(cf => cf.Flight)
            .WithMany(f => f.ClassFlights)
            .HasForeignKey(cf => cf.FlightId);
        builder.HasOne(cf => cf.FlightClass)
            .WithMany(fc => fc.ClassFlights)
            .HasForeignKey(cf => cf.FlightClassId);
        // Other configurations...
    }
}
