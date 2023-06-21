namespace Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;

class CityVehicleEntityTypeConfiguration : IEntityTypeConfiguration<CityVehicle>
{
  public void Configure(EntityTypeBuilder<CityVehicle> builder)
  {
    builder.HasKey(p => new { p.CityId,p.VehicleId});
  }
}