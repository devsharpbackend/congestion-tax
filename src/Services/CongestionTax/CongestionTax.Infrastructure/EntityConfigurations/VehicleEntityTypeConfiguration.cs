namespace Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;

class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.VehicleName).IsRequired().HasMaxLength(255);



        builder.HasMany<CityVehicle>()
        .WithOne()
        .IsRequired(false)
        .HasForeignKey("VehicleId");
    }
}
