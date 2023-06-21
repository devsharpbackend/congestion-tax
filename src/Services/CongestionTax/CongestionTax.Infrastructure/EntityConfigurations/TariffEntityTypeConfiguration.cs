namespace Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;

class TariffEntityTypeConfiguration : IEntityTypeConfiguration<Tariff>
{
  public void Configure(EntityTypeBuilder<Tariff> builder)
  {
    builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
    builder.Property(x=>x.Charge).HasColumnType("decimal(18,2)");

    builder.Ignore(b => b.DomainEvents);
  }
}
