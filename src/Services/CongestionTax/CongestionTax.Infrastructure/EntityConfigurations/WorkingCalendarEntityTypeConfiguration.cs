using Newtonsoft.Json;

namespace Fintranet.Services.CongestionTax.Infrastructure.EntityConfigurations;

class WorkingCalendarEntityTypeConfiguration : IEntityTypeConfiguration<WorkingCalendar>
{
    public void Configure(EntityTypeBuilder<WorkingCalendar> builder)
    {
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(p => p.WorkingCalendarName).IsRequired().HasMaxLength(255);

        // builder.Ignore(p => p.Holidays);

        builder
            .Property(e => e.StartDate)
            .HasConversion(
                v => v.ToDateTime(),   // Convert DateOnly to DateTime
                v => DateOnly.FromDateTime(v)  // Convert DateTime to DateOnly
            )
            .HasColumnType("date");   // Specify the column type as "date" in SQL Server

        builder
           .Property(e => e.EndDate)
           .HasConversion(
               v => v.ToDateTime(),
               v => DateOnly.FromDateTime(v)).HasColumnType("date");   // Specify the column type as "date" in SQL Server

        builder.Property<List<DateOnly>>("_holidays")
            .UsePropertyAccessMode(PropertyAccessMode.PreferField)
            .HasColumnName("Holidays").IsRequired(false)
            .HasConversion(
                 c => JsonConvert.SerializeObject(c),
                 c => JsonConvert.DeserializeObject<List<DateOnly>>(c));

        builder.Property(e => e.WorkingDays)
            .HasConversion<int>()
            .HasColumnName("WorkingDays");

        builder.Ignore(b => b.DomainEvents);
    }
}