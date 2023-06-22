
using Fintranet.Services.CongestionTax.Infrastructure.VehicleAggregate;

namespace Fintranet.Services.CongestionTax.Infrastructure.Factories;

public class ApplicationDbContextSeed : IApplicationDbContextSeed
{

    public async Task MigrateAndSeedAsync(IApplicationDbContext _context, ILogger<IApplicationDbContextSeed> logger)
    {
        ApplicationDbContext context = _context as ApplicationDbContext;
        if (context == null)
            return;

        await context.Database.MigrateAsync();
        try
        {
            // Seed Data for Vehicles
            if (!context.Set<Vehicle>().Any())
            {
                context.AddRange(GetPredefinedVehicles());
                await context.SaveChangesAsync();
            }
            if (!context.Set<WorkingCalendar>().Any())
            {
                context.AddRange(GetPredefinedWorkingCalendars());
                await context.SaveChangesAsync();
            }
            if (!context.Set<City>().Any())
            {
                context.AddRange(GetPredefinedCities());
                await context.SaveChangesAsync();
            }

           
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ApplicationDbContext));
        }


        IEnumerable<Vehicle> GetPredefinedVehicles()
        {
            return new List<Vehicle>()
        {
           new Vehicle("Motorcycle"),
           new Vehicle("Tractor"),
           new Vehicle("Emergency"),
           new Vehicle("Diplomat"),
           new Vehicle("Foreign"),
           new Vehicle("Military"),
           new Vehicle("Personal")
        };
        }
        IEnumerable<WorkingCalendar> GetPredefinedWorkingCalendars()
        {
            WorkingCalendar workingCalendar = new("GothenburgWorkingCalendar", new DateOnly(2013, 1, 1), new DateOnly(2013, 12, 28));
            // List of Holidays for 2013
            workingCalendar.AddHoliday(new DateOnly(2013, 01, 01));
            workingCalendar.AddHoliday(new DateOnly(2013, 06, 01));
            workingCalendar.AddHoliday(new DateOnly(2013, 01, 05));
            workingCalendar.AddHoliday(new DateOnly(2013, 06, 06));
            workingCalendar.AddHoliday(new DateOnly(2013, 06, 20));
            workingCalendar.AddHoliday(new DateOnly(2013, 06, 26));
            workingCalendar.AddHoliday(new DateOnly(2013, 12, 25));
            workingCalendar.AddHoliday(new DateOnly(2013, 12, 26));
            return new List<WorkingCalendar>()
        {
          workingCalendar
        };
        }
        IEnumerable<City> GetPredefinedCities()
        {
            City city = new("Gothenburg", context.Set<WorkingCalendar>().FirstOrDefault());
            // add Tariffs for Gothenburg City
            city.AddTariff(8, new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59));
            city.AddTariff(13, new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59));
            city.AddTariff(18, new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59));
            city.AddTariff(13, new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59));
            city.AddTariff(8, new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59));
            city.AddTariff(13, new TimeSpan(15, 00, 0), new TimeSpan(15, 29, 59));
            city.AddTariff(18, new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59));
            city.AddTariff(13, new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59));
            city.AddTariff(8, new TimeSpan(18, 0, 0), new TimeSpan(17, 29, 59));
            city.AddTariff(0, new TimeSpan(18, 30, 0), new TimeSpan(5, 59, 59));

            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Motorcycle"), false);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Tractor"), false);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Emergency"), false);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Diplomat"), false);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Foreign"), true);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Military"), false);
            city.AddVehicle(context.Set<Vehicle>().FirstOrDefault(p => p.VehicleName == "Personal"), true);



            return new List<City>()
        {
          city
        };
        }
    }


}
