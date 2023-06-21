namespace Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;

class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(p => p.CityName).IsRequired().HasMaxLength(255);

        builder.HasOne<WorkingCalendar>()
        .WithMany()
        .IsRequired(false)
        .HasForeignKey("WorkingCalendarId");

        // define Tariffs as Navigation Property
        var navigationTariffs = builder.Metadata.FindNavigation(nameof(City.Tariffs));
        navigationTariffs.SetPropertyAccessMode(PropertyAccessMode.Field);

        // define Tariffs as Navigation Property
        var navigationVehicles = builder.Metadata.FindNavigation(nameof(City.Vehicles));
        navigationVehicles.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(b => b.DomainEvents);
    }
}